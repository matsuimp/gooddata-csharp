using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GoodDataApi;
using GoodDataApi.ResourceExtensions;
using NUnit.Framework;

namespace GoodDataApiTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void DecodeFilter()
        {

        }


	    
	    private string Resolve(string expression, GoodDataConnection connection)
	    {
		    var regex = new Regex(@"\[/gdc/md/[A-Za-z0-9]+/obj/[0-9]+\]", RegexOptions.Compiled);
			var attributes = regex.Matches(expression).Cast<object>().Select(x => x.ToString()).ToArray();
			var resolved = expression;

			foreach (var attributeUriInBrackets in attributes)
			{
				var attributeUri = attributeUriInBrackets.Replace("[", string.Empty).Replace("]", string.Empty);
				var response = connection.Attribute.Get(attributeUri);
				resolved = resolved.Replace(attributeUriInBrackets, "[" + response.Content.Attribute.Meta.Title + "]");
				var elementResponse = connection.Attribute.GetElements(response.Content.Attribute.Content.DisplayForms[0].Links.Elements);
				foreach(var elemInfo in elementResponse.Content.AttributeElements.Elements)
				{
					resolved = resolved.Replace(elemInfo.Uri, elemInfo.Title);
				}
			}

		    return resolved;
	    }
    }

    public static class Extensions
    {
        public static ICollection<ICollection<T>> SplitList<T>(this ICollection<T> listToSplit, int countToTake)
        {
            ICollection<ICollection<T>> splitList = new List<ICollection<T>>();
            var countToSkip = 0;
            do
            {
                splitList.Add(listToSplit.Skip(countToSkip).Take(countToTake).ToList());
                countToSkip += countToTake;
            } while (countToSkip < listToSplit.Count);
            return splitList;
        }
    }
}