using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoodDataApi;
using GoodDataApi.Payload.User;
using NUnit.Framework;

namespace GoodDataApiTests
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public void GenerateScript()
		{
			var connection = new GoodDataConnection();
			var response = connection.User.DomainUsers(0, 5);
			// /gdc/account/profile/8427522a4506a22c156431ebd58f1f66
			// us03pustmnl2z7c9jm9vy1f9qiy2ve36
			Console.Out.WriteLine("hi");

			/gdc/projects/us03pustmnl2z7c9jm9vy1f9qiy2ve36/users
		}

		public class Found
		{
			public int AdminLoginId;
			public bool InGoodData;

			public Found(int id)
			{
				AdminLoginId = id;
				InGoodData = false;
			}
		}

		private static readonly Dictionary<string, Found>  Unassigned = new Dictionary<string, Found>
				                 {
	{"jkelt@groupcommerce.com", new Found(2)},
	{"gcadmin@groupcommerce.com", new Found(4)},
	{"testadmin1@groupcommerce.com", new Found(10)},
	{"ssapre@groupcommerce.com", new Found(22)},
	{"hglotzer@groupcommerce.com", new Found(51)},
	{"jkind+admin@groupcommerce.com", new Found(150)},
	{"mreust@groupcommerce.com", new Found(621)},
	{"apiuser@groupcommerce.com", new Found(649)},
	{"jbennett@groupcommerce.com", new Found(786)},
	{"reustmd@gmail.com", new Found(960)},
	{"sales@dc.com", new Found(1762)},
	{"mprice@groupcommerce.com", new Found(2984)},
	{"jcianflone+1@groupcommerce.com", new Found(3541)},
	{"jzeidner@gmail.com", new Found(3883)},
	{"vprasad@groupcommerce.com", new Found(3889)},
	{"tgriffin@groupcommerce.com", new Found(4877)},
	{"mchevett@groupcommerce.com", new Found(5197)},
	{"pnewell@groupcommerce.com", new Found(5297)},
	{"mlee@groupcommerce.com", new Found(5444)},
	{"ngupta@groupcommerce.com", new Found(5562)},
	{"nshah@groupcommerce.com", new Found(6577)},
	{"aglenn@groupcommerce.com", new Found(20050)},
	{"mmorris+gc@groupcommerce.com", new Found(46266)},
	{"shuynh@groupcommerce.com", new Found(47403)},
	{"jangel@groupcommerce.com", new Found(47422)},
	{"dmelinger+admin@groupcommerce.com", new Found(47968)},
	{"ebeckwith@groupcommerce.com", new Found(47985)},
	{"jkind@groupcommerce.com", new Found(48023)},
	{"eentin@groupcommerce.com", new Found(48025)},
	{"pponzeka@groupcommerce.com", new Found(48068)},
	{"hatndesign@gmail.com", new Found(48240)},
	{"jkind+gdtester@groupcommerce.com", new Found(48242)},
	{"amaindrault@groupcommerce.com", new Found(48245)},
	{"jkind+ssotester3@groupcommerce.com", new Found(48251)},
	{"jkind+ssotester5@groupcommerce.com", new Found(48252)},
	{"jkind+ssotester6@groupcommerce.com", new Found(48253)},
	{"mmcintyre@groupcommerce.com", new Found(48258)},
	{"evasilchenko@groupcommerce.com", new Found(48262)},
	{"jfairchild+admin@groupcommerce.com", new Found(48265)},
	{"qa+admin@groupcommerce.com", new Found(48266)},
	{"ccrews+admin@groupcommerce.com", new Found(48267)},
	{"gsadowski+admintest@groupcommerce.com", new Found(48268)},
	{"achusuei+test@groupcommerce.com", new Found(48270)},
	{"epaz@groupcommerce.com", new Found(48278)},
	{"alederman@groupcommerce.com", new Found(48279)},
	{"ssheridan@groupcommerce.com", new Found(48280)},
	{"gmedina@groupcommerce.com", new Found(48284)},
	{"gchen@groupcommerce.com", new Found(48287)},
	{"dyates@groupcommerce.com", new Found(48294)},
	{"avisan@groupcommerce.com", new Found(48301)},
	{"nshidore@groupcommerce.com", new Found(48303)},
	{"swhittingham@groupcommerce.com", new Found(48368)},
	{"gsadowski+gcadmin@groupcommerce.com", new Found(48370)},
	{"gpatel@groupcommerce.com", new Found(48380)},
	{"wcope@groupcommerce.com", new Found(48381)},
	{"amunigala@groupcommerce.com", new Found(48387)},
	{"mmatsui@groupcommerce.com", new Found(48395)},
	{"dschaub@groupcommerce.com", new Found(48396)},
	{"vkennedy@groupcommerce.com", new Found(48398)},
	{"gbarkeling@groupcommerce.com", new Found(48400)},
	{"hwolfeld@groupcommerce.com", new Found(48401)},
	{"nblumenthal@groupcommerce.com", new Found(48405)},
	{"alo@groupcommerce.com", new Found(48406)},
	{"pjadhav@groupcommerce.com", new Found(48407)},
	{"smoore@groupcommerce.com", new Found(48410)},
	{"awolf@groupcommerce.com", new Found(48413)},
	{"mreust+email@groupcommerce.com", new Found(48414)},
	{"awolf+pubadmintestnosegments@groupcommerce.com", new Found(48418)},
	{"avisan+a@groupcommerce.com", new Found(48421)},
	{"awolf+test@groupcommerce.com", new Found(48441)},
	{"mwallace@groupcommerce.com", new Found(48444)},
	{"gchen+uk@groupcommerce.com", new Found(48446)},
	{"nderenzis@groupcommerce.com", new Found(48447)},
	{"sstreiger@groupcommerce.com", new Found(48448)},
	{"mmorris@groupcommerce.com", new Found(48449)},
				                 };
	}
}
