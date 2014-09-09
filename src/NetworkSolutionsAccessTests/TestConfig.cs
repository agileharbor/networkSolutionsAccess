using LINQtoCSV;

namespace NetworkSolutionsAccessTests
{
	internal class TestConfig
	{
		[ CsvColumn( Name = "ApplicationName", FieldIndex = 1 ) ]
		public string ApplicationName{ get; set; }

		[ CsvColumn( Name = "Certificate", FieldIndex = 2 ) ]
		public string Certificate{ get; set; }

		[ CsvColumn( Name = "UserKey", FieldIndex = 3 ) ]
		public string UserKey{ get; set; }

		[ CsvColumn( Name = "UserToken", FieldIndex = 4 ) ]
		public string UserToken{ get; set; }
	}
}