using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoodDataApi;
using GoodDataApi.Payload.Filters;
using GoodDataApi.Payload.User;
using NUnit.Framework;

namespace GoodDataApiTests
{
    [TestFixture]
    public class Tests
    {
	    private const string DevProjectId = "us03pustmnl2z7c9jm9vy1f9qiy2ve36";
        [Test]
        public void GenerateScript()
        {
	        var all = new GoodDataConnection().DomainUser.GetAllUsers();
			foreach (var user in all)
				Console.Out.WriteLine(user.Email);
        }
    }
}
