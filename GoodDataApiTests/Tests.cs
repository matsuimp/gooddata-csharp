using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GoodDataApi;
using GoodDataApi.Payload.Report;
using GoodDataApi.Payload.User;
using NUnit.Framework;

namespace GoodDataApiTests
{
	[TestFixture]
	public class Tests
	{

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