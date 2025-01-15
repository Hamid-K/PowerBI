using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200003B RID: 59
	public static class ANXmlaConstants
	{
		// Token: 0x040000AF RID: 175
		public const string XmlaMaxParallelismNodeName = "Parallel";

		// Token: 0x040000B0 RID: 176
		public const string XmlaMaxParallelismAttributeName = "MaxParallel";

		// Token: 0x040000B1 RID: 177
		public const string TMXmlaMaxParallelismNodeName = "MaxParallelism";

		// Token: 0x040000B2 RID: 178
		public const string XmlaProcessCommandNameSpace = "http://schemas.microsoft.com/analysisservices/2003/engine";

		// Token: 0x040000B3 RID: 179
		public const string TMXmlaProcessCommandNameSpace = "http://schemas.microsoft.com/analysisservices/2014/engine";

		// Token: 0x040000B4 RID: 180
		public const string ProcessFullXmlaTemplate = "<Process xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                <Type>ProcessFull</Type>\r\n                <Object>\r\n                <DatabaseID>__PowerBI_Database_Identifier__</DatabaseID></Object></Process>";

		// Token: 0x040000B5 RID: 181
		public const string ProcessFullTMXmlaTemplate = "<Batch Transaction=\"false\" xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n            <Refresh xmlns=\"http://schemas.microsoft.com/analysisservices/2014/engine\">\r\n            <DatabaseID>__PowerBI_Database_Identifier__</DatabaseID>\r\n            <Model>\r\n              <!-- Begin Refresh Model schema -->\r\n              <xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:sql=\"urn:schemas-microsoft-com:xml-sql\">\r\n                <xs:element>\r\n                  <xs:complexType>\r\n                    <xs:sequence>\r\n                      <xs:element type=\"row\" />\r\n                    </xs:sequence>\r\n                  </xs:complexType>\r\n                </xs:element>\r\n                <xs:complexType name=\"row\">\r\n                  <xs:sequence>\r\n                    <xs:element name=\"RefreshType\" type=\"xs:long\" sql:field=\"RefreshType\" minOccurs=\"0\" />\r\n                  </xs:sequence>\r\n                </xs:complexType>\r\n              </xs:schema>\r\n              <!-- End Refresh Model schema -->\r\n              <row xmlns=\"urn:schemas-microsoft-com:xml-analysis:rowset\">\r\n                <RefreshType>1</RefreshType>\r\n              </row>\r\n            </Model>          \r\n          </Refresh>           \r\n        </Batch>";

		// Token: 0x040000B6 RID: 182
		public const string DataSourceXmlaTemplate = "<DataSource xsi:type=\"RelationalDataSource\">\r\n                    <ID>{0}</ID>\r\n                    <Name>{1}</Name>\r\n                    <ConnectionString>{2}</ConnectionString>\r\n                    <ManagedProvider>{3}</ManagedProvider>\r\n            </DataSource>";

		// Token: 0x040000B7 RID: 183
		public const string RecalcCommandTemplate = "\r\n            <Process xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                <Type>ProcessRecalc</Type>\r\n                <Object>\r\n                    <DatabaseID>{0}</DatabaseID>\r\n                </Object>\r\n            </Process>";

		// Token: 0x040000B8 RID: 184
		public const string ProcessPartitionTemplate = "\r\n            <Process>\r\n                <Type>ProcessData</Type>\r\n                <Object>\r\n                    <DatabaseID>{0}</DatabaseID>\r\n                    <CubeID>{1}</CubeID>\r\n                    <MeasureGroupID>{2}</MeasureGroupID>\r\n                    <PartitionID>{3}</PartitionID>\r\n                </Object>\r\n            </Process>";

		// Token: 0x040000B9 RID: 185
		public const string BatchProcessTemplate = "\r\n            <Batch xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:ddl2=\"http://schemas.microsoft.com/analysisservices/2003/engine/2\" xmlns:ddl2_2=\"http://schemas.microsoft.com/analysisservices/2003/engine/2/2\" xmlns:ddl100_100=\"http://schemas.microsoft.com/analysisservices/2008/engine/100/100\" xmlns:ddl200=\"http://schemas.microsoft.com/analysisservices/2010/engine/200\" xmlns:ddl200_200=\"http://schemas.microsoft.com/analysisservices/2010/engine/200/200\" xmlns:ddl300=\"http://schemas.microsoft.com/analysisservices/2011/engine/300\" xmlns:ddl300_300=\"http://schemas.microsoft.com/analysisservices/2011/engine/300/300\" Transaction=\"true\">\r\n                <Parallel MaxParallel=\"{3}\">{1}</Parallel>\r\n                <Process xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                <Type>ProcessRecalc</Type>\r\n                <Object>\r\n                    <DatabaseID>{0}</DatabaseID>\r\n                </Object>\r\n                </Process>\r\n                <DataSources>{2}</DataSources>\r\n                <Bindings>{4}</Bindings>\r\n            </Batch>";

		// Token: 0x040000BA RID: 186
		public const string BatchTabularJsonSchemaProcessTemplate = "<Batch xmlns='http://schemas.microsoft.com/analysisservices/2003/engine' Transaction='true'>\r\n                {0}\r\n            </Batch>";

		// Token: 0x040000BB RID: 187
		public const string BindingXmlaTemplate = "<Binding>\r\n                    <DatabaseID>{0}</DatabaseID>\r\n                    <CubeID>{1}</CubeID>\r\n                    <MeasureGroupID>{2}</MeasureGroupID>\r\n                    <PartitionID>{3}</PartitionID>\r\n                    <Source xsi:type=\"QueryBinding\">\r\n                        <DataSourceID>{4}</DataSourceID>\r\n                        <QueryDefinition>select * from {5}</QueryDefinition>\r\n                    </Source>\r\n            </Binding>";

		// Token: 0x040000BC RID: 188
		public const string DataSourceTMXmlaTemplate = "<Binding>\r\n               <ObjectID>{0}</ObjectID>\r\n              <DataSources>\r\n                 <!-- Begin Bindings DataSource schema -->\r\n                      <xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:sql=\"urn:schemas-microsoft-com:xml-sql\">\r\n                          <xs:element>\r\n                            <xs:complexType>\r\n                              <xs:sequence>\r\n                                <xs:element type=\"row\" />\r\n                               </xs:sequence>\r\n                            </xs:complexType>\r\n                          </xs:element>\r\n                          <xs:complexType name=\"row\">\r\n                            <xs:sequence>\r\n                            <xs:element name=\"ID\" type=\"xs:unsignedLong\" sql:field=\"ID\" minOccurs=\"0\" />\r\n                            <xs:element name=\"ID.DataSource\" type=\"xs:string\" sql:field=\"ID.DataSource\" minOccurs=\"0\" />\r\n                            <xs:element name=\"ConnectionString\" type=\"xs:string\" sql:field=\"ConnectionString\" minOccurs=\"0\" />\r\n                            <xs:element name=\"ImpersonationMode\" type=\"xs:long\" sql:field=\"ImpersonationMode\" minOccurs=\"0\" />\r\n                            <xs:element name=\"Account\" type=\"xs:string\" sql:field=\"Account\" minOccurs=\"0\" />\r\n                            <xs:element name=\"Password\" type=\"xs:string\" sql:field=\"Password\" minOccurs=\"0\" />\r\n                            <xs:element name=\"MaxConnections\" type=\"xs:int\" sql:field=\"MaxConnections\" minOccurs=\"0\" />\r\n                            <xs:element name=\"Isolation\" type=\"xs:long\" sql:field=\"Isolation\" minOccurs=\"0\" />\r\n                          <xs:element name=\"Timeout\" type=\"xs:int\" sql:field=\"Timeout\" minOccurs=\"0\" />\r\n                          <xs:element name=\"Provider\" type=\"xs:string\" sql:field=\"Provider\" minOccurs=\"0\" />\r\n                            </xs:sequence>\r\n                          </xs:complexType>\r\n                        </xs:schema>\r\n                 <!-- End Bindings DataSource schema -->\r\n                    <row xmlns=\"urn:schemas-microsoft-com:xml-analysis:rowset\">\r\n                       <ID>{1}</ID>\r\n                       <ConnectionString>{2}</ConnectionString>\r\n                       <Provider>{3}</Provider>\r\n                   </row>\r\n                  </DataSources>\r\n               </Binding>";

		// Token: 0x040000BD RID: 189
		public const string ProcessPartitionTMTemplate = "\r\n                <row xmlns=\"urn:schemas-microsoft-com:xml-analysis:rowset\">\r\n                    <ID>{0}</ID>\r\n                    <RefreshType>4</RefreshType>\r\n                </row>";

		// Token: 0x040000BE RID: 190
		public const string ProcessTMXmlaTemplate = "<Refresh xmlns=\"http://schemas.microsoft.com/analysisservices/2014/engine\">\r\n                <DatabaseID>{0}</DatabaseID>\r\n                <MaxParallelism>{1}</MaxParallelism> \r\n                <Model>\r\n              <!-- Begin Refresh Model schema -->\r\n              <xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:sql=\"urn:schemas-microsoft-com:xml-sql\">\r\n                <xs:element>\r\n                  <xs:complexType>\r\n                    <xs:sequence>\r\n                      <xs:element type=\"row\" />\r\n                    </xs:sequence>\r\n                  </xs:complexType>\r\n                </xs:element>\r\n                <xs:complexType name=\"row\">\r\n                  <xs:sequence>\r\n                    <xs:element name=\"RefreshType\" type=\"xs:long\" sql:field=\"RefreshType\" minOccurs=\"0\" />\r\n                  </xs:sequence>\r\n                </xs:complexType>\r\n              </xs:schema>\r\n              <!-- End Refresh Model schema -->\r\n              <row xmlns=\"urn:schemas-microsoft-com:xml-analysis:rowset\">\r\n                <RefreshType>3</RefreshType>\r\n              </row>\r\n            </Model> \r\n                <Partitions>\r\n                 <!-- Begin Refresh Partition schema -->\r\n                     <xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:sql=\"urn:schemas-microsoft-com:xml-sql\">\r\n                      <xs:element>\r\n                        <xs:complexType>\r\n                          <xs:sequence>\r\n                            <xs:element type=\"row\" />\r\n                          </xs:sequence>\r\n                        </xs:complexType>\r\n                      </xs:element>\r\n                      <xs:complexType name=\"row\">\r\n                        <xs:sequence>\r\n                          <xs:element name=\"ID\" type=\"xs:unsignedLong\" sql:field=\"ID\" minOccurs=\"0\" />\r\n                          <xs:element name=\"ID.Table\" type=\"xs:string\" sql:field=\"ID.Table\" minOccurs=\"0\" />\r\n                          <xs:element name=\"ID.Partition\" type=\"xs:string\" sql:field=\"ID.Partition\" minOccurs=\"0\" />\r\n                          <xs:element name=\"RefreshType\" type=\"xs:long\" sql:field=\"RefreshType\" minOccurs=\"0\" />\r\n                        </xs:sequence>\r\n                      </xs:complexType>\r\n                    </xs:schema>\r\n                  <!-- End Refresh Partition schema -->\r\n                {2}\r\n                </Partitions>\r\n                <Bindings>\r\n                {3}\r\n                </Bindings>\r\n              </Refresh>";

		// Token: 0x040000BF RID: 191
		public const string DiscoverDataSourceTMXmlaTemplate = "<Batch Transaction=\"false\" xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                    <Discover xmlns=\"urn:schemas-microsoft-com:xml-analysis\">\r\n                      <RequestType>TMSCHEMA_DATA_SOURCES</RequestType>\r\n                      <Restrictions>\r\n                        <RestrictionList>\r\n                          <DatabaseName>{{DATABASE_NAME}}</DatabaseName>\r\n                        </RestrictionList>\r\n                      </Restrictions>\r\n                      <Properties>\r\n                        <PropertyList />\r\n                      </Properties>\r\n                    </Discover>\r\n                  </Batch>";

		// Token: 0x040000C0 RID: 192
		public const string DiscoverPostV1DataSourceXmlaTemplate = "<Batch Transaction=\"false\" xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                    <Discover xmlns=\"urn:schemas-microsoft-com:xml-analysis\">\r\n                      <RequestType>DISCOVER_POWERBI_DATASOURCES</RequestType>\r\n                      <Restrictions>\r\n                        <RestrictionList />\r\n                      </Restrictions>\r\n                      <Properties>\r\n                        <PropertyList />\r\n                      </Properties>\r\n                    </Discover>\r\n                  </Batch>";

		// Token: 0x040000C1 RID: 193
		public const string IncrementalRefreshJsonTemplate = "{\r\n                \"sequence\": \r\n                {\r\n                    \"maxParallelism\": {MAXPARALLELISM},\r\n                    \"operations\": \r\n                    [\r\n                        {\r\n                            \"refresh\": \r\n                            {\r\n                                \"type\": \"full\",\r\n                                \"applyRefreshPolicy\": true,\r\n                                \"effectiveDate\": \"{EFFECTIVEDATE}\",\r\n                                \"objects\": \r\n                                [\r\n                                    {\r\n                                        \"database\": \"{DATABASE}\"\r\n                                    }\r\n                                ]\r\n                            }\r\n                        }\r\n                    ]\r\n                }\r\n            } ";

		// Token: 0x040000C2 RID: 194
		public const string MaxParallelismTag = "{MAXPARALLELISM}";

		// Token: 0x040000C3 RID: 195
		public const string EffectiveDateTag = "{EFFECTIVEDATE}";

		// Token: 0x040000C4 RID: 196
		public const string DatabaseTag = "{DATABASE}";

		// Token: 0x040000C5 RID: 197
		public const int DATABASE_NOT_FOUND_VIA_AMO_CONNECTION_ERROR_CODE = -2146233088;

		// Token: 0x040000C6 RID: 198
		public const string XMLA_NS = "http://schemas.microsoft.com/analysisservices/2003/engine";

		// Token: 0x040000C7 RID: 199
		public const string XMLA_NS14 = "http://schemas.microsoft.com/analysisservices/2014/engine";

		// Token: 0x040000C8 RID: 200
		public const int REQUIRED_COMPATIBILITY_LEVEL = 1200;

		// Token: 0x040000C9 RID: 201
		public const string XMLA_DATABASE_NAME_PLACEHOLDER = "{{DATABASE_NAME}}";
	}
}
