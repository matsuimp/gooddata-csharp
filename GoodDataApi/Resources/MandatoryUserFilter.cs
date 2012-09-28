namespace GoodDataApi.Resources
{
	public interface IMandatoryUserFilter 
	{
		
	}

	internal class MandatoryUserFilter : IMandatoryUserFilter
	{
		private readonly IGoodDataConnection _connection;
		private const string GetAllUserFilters = @"/gdc/md/us03pustmnl2z7c9jm9vy1f9qiy2ve36/userfilters";

		public MandatoryUserFilter(IGoodDataConnection connection)
		{
			_connection = connection;
		}


		public void AllFilters()
		{
			
		}
	}
}