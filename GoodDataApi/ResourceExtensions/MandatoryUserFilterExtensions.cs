using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GoodDataApi.Payload.Attribute;
using GoodDataApi.Resources;
using log4net;

namespace GoodDataApi.ResourceExtensions
{
	public class MandatoryUserFilterExtensions
	{
		private static readonly ILog Logger = LogManager.GetLogger(typeof (MandatoryUserFilterExtensions));
		private static readonly Regex AttributeRegex = new Regex(@"(?<=\[)/gdc/md/[A-Za-z0-9]+/obj/[0-9]+(?=\])", RegexOptions.Compiled);
		private static readonly Regex ElementRegex = new Regex(@"(?<=\[)/gdc/md/[A-Za-z0-9]+/obj/[0-9]+/elements\?id=[0-9]+(?=\])", RegexOptions.Compiled);

		public string[] Decode(GoodDataConnection connection, IEnumerable<string> filterUris)
		{
			var expressions = filterUris.Select(x => TryGetExpression(connection.MandatoryUserFilter, x)).ToArray();
			var lookups = BuildReplacementDictionary(connection, expressions);

			for (int index = 0; index < expressions.Length; index++)
			{
				expressions[index] = Replace(lookups, expressions[index]);
			}

			return expressions;
		}

		public string Decode(GoodDataConnection connection, string filterUri)
		{
			return Decode(connection, new[] {filterUri})[0];
		}

		private static Dictionary<string, string> BuildReplacementDictionary(GoodDataConnection connection, IEnumerable<string> expressions)
		{
			var combinedExpresison = string.Join(" ", expressions.Where(x => !string.IsNullOrWhiteSpace(x)));
			var attributes = AttributeRegex
				.Matches(combinedExpresison)
				.Cast<object>()
				.Select(x => x.ToString())
				.Distinct()
				.Select(x => TryGetAttribute(connection.Attribute, x))
				.ToArray();


			var usedElements = ElementRegex.Matches(combinedExpresison).Cast<object>().Select(x => x.ToString()).Distinct().ToArray();
			var uriToReplacement = usedElements.ToDictionary(elem => elem);

			foreach (var attribute in attributes)
			{
				if (null != attribute)
				{
					uriToReplacement[string.Format("[{0}]", attribute.Attribute.Meta.Uri)] = string.Format("[{0}]", attribute.Attribute.Meta.Title);
					try
					{
						var elements = connection.Attribute.GetElements(attribute.Attribute.Content.DisplayForms[0].Links.Elements).AssertSuccess().AttributeElements.Elements;
						if (null != elements)
						{
							foreach (var elemInfo in elements)
							{
								if (uriToReplacement.ContainsKey(elemInfo.Uri))
								{
									uriToReplacement[elemInfo.Uri] = elemInfo.Title;
								}
							}
						}
					}
					catch (Exception e)
					{
						Logger.Warn(e);
					}
				}
			}

			return uriToReplacement;
		}

		private static string Replace(Dictionary<string, string> uriToTitle, string expression)
		{
			if (string.IsNullOrWhiteSpace(expression))
				return expression;

			return uriToTitle.Aggregate(expression, (current, pair) => current.Replace(pair.Key, pair.Value));
		}

		private static string TryGetExpression(IMandatoryUserFilter filterApi, string filterUri)
		{
			try
			{
				return filterApi
					.Get(filterUri)
					.AssertSuccess()
					.UserFilter.Content.Expression;
			}
			catch (Exception e)
			{
				Logger.Warn(e);
				return null;
			}
		}

		private static AttributeResponse TryGetAttribute(IGoodDataAttribute attributeApi, string uri)
		{
			try
			{
				return attributeApi.Get(uri).AssertSuccess();
			}
			catch (Exception e)
			{
				Logger.Warn(e);
				return null;
			}
		}
	}
}