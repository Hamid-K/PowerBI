using System;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200003E RID: 62
	internal static class XmlaConstants
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00019554 File Offset: 0x00017754
		internal static NameTable GetNameTable()
		{
			NameTable nameTable = new NameTable();
			nameTable.Add("");
			nameTable.Add("http://schemas.xmlsoap.org/soap/envelope/");
			nameTable.Add("urn:schemas-microsoft-com:xml-analysis");
			nameTable.Add("http://schemas.microsoft.com/analysisservices/2003/xmla");
			nameTable.Add("http://schemas.microsoft.com/analysisservices/2003/ext");
			nameTable.Add("http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
			nameTable.Add("urn:schemas-microsoft-com:xml-analysis:fault");
			nameTable.Add("http://schemas.microsoft.com/analysisservices/2003/engine");
			nameTable.Add("urn:schemas-microsoft-com:xml-analysis:rowset");
			nameTable.Add("urn:schemas-microsoft-com:xml-sql");
			nameTable.Add("urn:schemas-microsoft-com:xml-analysis:mddataset");
			nameTable.Add("http://www.w3.org/2001/XMLSchema");
			nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			nameTable.Add("urn:schemas-microsoft-com:xml-analysis:empty");
			nameTable.Add("urn:schemas-microsoft-com:xml-analysis:exception");
			nameTable.Add("urn:schemas-microsoft-com:xml-analysis:xmlDocumentDataset");
			nameTable.Add("xmlns:xsd");
			nameTable.Add("xmlns:xsi");
			nameTable.Add("xsi:type");
			nameTable.Add("mustUnderstand");
			nameTable.Add("SessionId");
			nameTable.Add("Description");
			nameTable.Add("Source");
			nameTable.Add("HelpFile");
			nameTable.Add("WarningCode");
			nameTable.Add("Transaction");
			nameTable.Add("ProcessAffectedObjects");
			nameTable.Add("KeyErrorLimit");
			nameTable.Add("KeyErrorLogFile");
			nameTable.Add("ErrorCode");
			nameTable.Add("0");
			nameTable.Add("1");
			nameTable.Add("sx");
			nameTable.Add("xpress");
			nameTable.Add("rt");
			nameTable.Add("xmlns");
			nameTable.Add("Batch");
			nameTable.Add("Discover");
			nameTable.Add("RequestType");
			nameTable.Add("Restrictions");
			nameTable.Add("RestrictionList");
			nameTable.Add("row");
			nameTable.Add("SessionID");
			nameTable.Add("ConnectionID");
			nameTable.Add("SPID");
			nameTable.Add("CancelAssociated");
			nameTable.Add("Exception");
			nameTable.Add("Messages");
			nameTable.Add("Cancel");
			nameTable.Add("Header");
			nameTable.Add("Session");
			nameTable.Add("BeginSession");
			nameTable.Add("EndSession");
			nameTable.Add("Envelope");
			nameTable.Add("Authenticate");
			nameTable.Add("SspiHandshake");
			nameTable.Add("Body");
			nameTable.Add("Fault");
			nameTable.Add("return");
			nameTable.Add("results");
			nameTable.Add("root");
			nameTable.Add("faultcode");
			nameTable.Add("faultstring");
			nameTable.Add("faultactor");
			nameTable.Add("errortype");
			nameTable.Add("isprimary");
			nameTable.Add("detail");
			nameTable.Add("schema");
			nameTable.Add("Error");
			nameTable.Add("Location");
			nameTable.Add("Start");
			nameTable.Add("End");
			nameTable.Add("Line");
			nameTable.Add("Column");
			nameTable.Add("LineOffset");
			nameTable.Add("TextLength");
			nameTable.Add("Warning");
			nameTable.Add("Execute");
			nameTable.Add("Command");
			nameTable.Add("Statement");
			nameTable.Add("Properties");
			nameTable.Add("PropertyList");
			nameTable.Add("Parameters");
			nameTable.Add("Parameter");
			nameTable.Add("Name");
			nameTable.Add("Value");
			nameTable.Add("AuthenticateResponse");
			nameTable.Add("ExecuteResponse");
			nameTable.Add("DiscoverResponse");
			nameTable.Add("uuid");
			nameTable.Add("xmlDocument");
			nameTable.Add("type");
			nameTable.Add("unbounded");
			nameTable.Add("field");
			nameTable.Add("nil");
			nameTable.Add("true");
			nameTable.Add("OlapInfo");
			nameTable.Add("AxesInfo");
			nameTable.Add("AxisInfo");
			nameTable.Add("HierarchyInfo");
			nameTable.Add("CellInfo");
			nameTable.Add("SlicerAxis");
			nameTable.Add("Axes");
			nameTable.Add("Axis");
			nameTable.Add("Tuples");
			nameTable.Add("Tuple");
			nameTable.Add("Member");
			nameTable.Add("CellData");
			nameTable.Add("Cell");
			nameTable.Add("CubeInfo");
			nameTable.Add("Cube");
			nameTable.Add("CubeName");
			nameTable.Add("LastDataUpdate");
			nameTable.Add("LastSchemaUpdate");
			nameTable.Add("name");
			nameTable.Add("Hierarchy");
			nameTable.Add("CellOrdinal");
			nameTable.Add("Value");
			nameTable.Add("FmtValue");
			nameTable.Add("UName");
			nameTable.Add("LName");
			nameTable.Add("LNum");
			nameTable.Add("Caption");
			nameTable.Add("Parent");
			nameTable.Add("Description");
			nameTable.Add("DisplayInfo");
			return nameTable;
		}

		// Token: 0x04000297 RID: 663
		internal const string EmptyNamespace = "";

		// Token: 0x04000298 RID: 664
		internal const string EnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x04000299 RID: 665
		internal const string XmlaNamespace = "urn:schemas-microsoft-com:xml-analysis";

		// Token: 0x0400029A RID: 666
		internal const string XmlaExtensionsNamespace = "http://schemas.microsoft.com/analysisservices/2003/xmla";

		// Token: 0x0400029B RID: 667
		internal const string PrivateExtensionsNamespace = "http://schemas.microsoft.com/analysisservices/2003/ext";

		// Token: 0x0400029C RID: 668
		internal const string MultipleResultsNamespace = "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults";

		// Token: 0x0400029D RID: 669
		internal const string FaultNamespace = "urn:schemas-microsoft-com:xml-analysis:fault";

		// Token: 0x0400029E RID: 670
		internal const string DdlNamespace = "http://schemas.microsoft.com/analysisservices/2003/engine";

		// Token: 0x0400029F RID: 671
		internal const string DdlNamespace2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2";

		// Token: 0x040002A0 RID: 672
		internal const string DdlNamespace2_2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2/2";

		// Token: 0x040002A1 RID: 673
		internal const string DdlNamespace100 = "http://schemas.microsoft.com/analysisservices/2008/engine/100";

		// Token: 0x040002A2 RID: 674
		internal const string DdlNamespace100_100 = "http://schemas.microsoft.com/analysisservices/2008/engine/100/100";

		// Token: 0x040002A3 RID: 675
		internal const string DdlNamespace200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200";

		// Token: 0x040002A4 RID: 676
		internal const string DdlNamespace200_200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200/200";

		// Token: 0x040002A5 RID: 677
		internal const string DdlNamespace300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300";

		// Token: 0x040002A6 RID: 678
		internal const string DdlNamespace300_300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300/300";

		// Token: 0x040002A7 RID: 679
		internal const string DdlNamespace400 = "http://schemas.microsoft.com/analysisservices/2012/engine/400";

		// Token: 0x040002A8 RID: 680
		internal const string DdlNamespace400_400 = "http://schemas.microsoft.com/analysisservices/2012/engine/400/400";

		// Token: 0x040002A9 RID: 681
		internal const string DdlNamespace410 = "http://schemas.microsoft.com/analysisservices/2012/engine/410";

		// Token: 0x040002AA RID: 682
		internal const string DdlNamespace410_410 = "http://schemas.microsoft.com/analysisservices/2012/engine/410/410";

		// Token: 0x040002AB RID: 683
		internal const string DdlNamespace500 = "http://schemas.microsoft.com/analysisservices/2013/engine/500";

		// Token: 0x040002AC RID: 684
		internal const string DdlNamespace500_500 = "http://schemas.microsoft.com/analysisservices/2013/engine/500/500";

		// Token: 0x040002AD RID: 685
		internal const string DdlNamespace600 = "http://schemas.microsoft.com/analysisservices/2013/engine/600";

		// Token: 0x040002AE RID: 686
		internal const string DdlNamespace600_600 = "http://schemas.microsoft.com/analysisservices/2013/engine/600/600";

		// Token: 0x040002AF RID: 687
		internal const string DdlNamespace700 = "http://schemas.microsoft.com/analysisservices/2018/engine/700";

		// Token: 0x040002B0 RID: 688
		internal const string DdlNamespace700_700 = "http://schemas.microsoft.com/analysisservices/2018/engine/700/700";

		// Token: 0x040002B1 RID: 689
		internal const string DdlNamespace800 = "http://schemas.microsoft.com/analysisservices/2018/engine/800";

		// Token: 0x040002B2 RID: 690
		internal const string DdlNamespace800_800 = "http://schemas.microsoft.com/analysisservices/2018/engine/800/800";

		// Token: 0x040002B3 RID: 691
		internal const string DdlNamespace900 = "http://schemas.microsoft.com/analysisservices/2019/engine/900";

		// Token: 0x040002B4 RID: 692
		internal const string DdlNamespace900_900 = "http://schemas.microsoft.com/analysisservices/2019/engine/900/900";

		// Token: 0x040002B5 RID: 693
		internal const string DdlNamespace910 = "http://schemas.microsoft.com/analysisservices/2020/engine/910";

		// Token: 0x040002B6 RID: 694
		internal const string DdlNamespace910_910 = "http://schemas.microsoft.com/analysisservices/2020/engine/910/910";

		// Token: 0x040002B7 RID: 695
		internal const string DdlNamespace920 = "http://schemas.microsoft.com/analysisservices/2020/engine/920";

		// Token: 0x040002B8 RID: 696
		internal const string DdlNamespace920_920 = "http://schemas.microsoft.com/analysisservices/2020/engine/920/920";

		// Token: 0x040002B9 RID: 697
		internal const string DdlNamespace921 = "http://schemas.microsoft.com/analysisservices/2021/engine/921";

		// Token: 0x040002BA RID: 698
		internal const string DdlNamespace921_921 = "http://schemas.microsoft.com/analysisservices/2021/engine/921/921";

		// Token: 0x040002BB RID: 699
		internal const string DdlNamespace922 = "http://schemas.microsoft.com/analysisservices/2022/engine/922";

		// Token: 0x040002BC RID: 700
		internal const string DdlNamespace922_922 = "http://schemas.microsoft.com/analysisservices/2022/engine/922/922";

		// Token: 0x040002BD RID: 701
		internal const string RowsetNamespace = "urn:schemas-microsoft-com:xml-analysis:rowset";

		// Token: 0x040002BE RID: 702
		internal const string SqlNamespace = "urn:schemas-microsoft-com:xml-sql";

		// Token: 0x040002BF RID: 703
		internal const string DatasetNamespace = "urn:schemas-microsoft-com:xml-analysis:mddataset";

		// Token: 0x040002C0 RID: 704
		internal const string XsdNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x040002C1 RID: 705
		internal const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040002C2 RID: 706
		internal const string EmptyResultNamespace = "urn:schemas-microsoft-com:xml-analysis:empty";

		// Token: 0x040002C3 RID: 707
		internal const string ExceptionNamespace = "urn:schemas-microsoft-com:xml-analysis:exception";

		// Token: 0x040002C4 RID: 708
		internal const string XmlDocumentColumnDatasetNamespace = "urn:schemas-microsoft-com:xml-analysis:xmlDocumentDataset";

		// Token: 0x040002C5 RID: 709
		internal const string XsdNamespaceAttribute = "xmlns:xsd";

		// Token: 0x040002C6 RID: 710
		internal const string XsiNamespaceAttribute = "xmlns:xsi";

		// Token: 0x040002C7 RID: 711
		internal const string TypeAttribute = "xsi:type";

		// Token: 0x040002C8 RID: 712
		internal const string NullTypeAttribute = "xsi:nil";

		// Token: 0x040002C9 RID: 713
		internal const string MustUnderstandAttribute = "mustUnderstand";

		// Token: 0x040002CA RID: 714
		internal const string SessionIdAttribute = "SessionId";

		// Token: 0x040002CB RID: 715
		internal const string SequenceAttribute = "Sequence";

		// Token: 0x040002CC RID: 716
		internal const string DescriptionElementAttribute = "Description";

		// Token: 0x040002CD RID: 717
		internal const string SourceAttribute = "Source";

		// Token: 0x040002CE RID: 718
		internal const string HelpFileAttribute = "HelpFile";

		// Token: 0x040002CF RID: 719
		internal const string WarningCodeAttribute = "WarningCode";

		// Token: 0x040002D0 RID: 720
		internal const string TransactionAttribute = "Transaction";

		// Token: 0x040002D1 RID: 721
		internal const string ProcessAffectedObjectsAttribute = "ProcessAffectedObjects";

		// Token: 0x040002D2 RID: 722
		internal const string SkipVolatileObjectsAttribute = "SkipVolatileObjects";

		// Token: 0x040002D3 RID: 723
		internal const string ErrorCodeElementAttribute = "ErrorCode";

		// Token: 0x040002D4 RID: 724
		internal const string MustUnderstandFalse = "0";

		// Token: 0x040002D5 RID: 725
		internal const string MustUnderstandTrue = "1";

		// Token: 0x040002D6 RID: 726
		internal const string BinaryXmlCapabilityValue = "sx";

		// Token: 0x040002D7 RID: 727
		internal const string CompressionCapabilityValue = "xpress";

		// Token: 0x040002D8 RID: 728
		internal const string SchannelAuthProtocol = "Schannel";

		// Token: 0x040002D9 RID: 729
		internal const string RestrictionTypeNSPrefix = "rt";

		// Token: 0x040002DA RID: 730
		internal const string XmlnsPrefix = "xmlns";

		// Token: 0x040002DB RID: 731
		internal const string SoapPrefix = "soap";

		// Token: 0x040002DC RID: 732
		internal const string BatchElement = "Batch";

		// Token: 0x040002DD RID: 733
		internal const string KeyErrorLimitElement = "KeyErrorLimit";

		// Token: 0x040002DE RID: 734
		internal const string KeyErrorLogFileElement = "KeyErrorLogFile";

		// Token: 0x040002DF RID: 735
		internal const string DiscoverElement = "Discover";

		// Token: 0x040002E0 RID: 736
		internal const string RequestTypeElement = "RequestType";

		// Token: 0x040002E1 RID: 737
		internal const string RestrictionsElement = "Restrictions";

		// Token: 0x040002E2 RID: 738
		internal const string RestrictionListElement = "RestrictionList";

		// Token: 0x040002E3 RID: 739
		internal const string RowElement = "row";

		// Token: 0x040002E4 RID: 740
		internal const string SessionIDElement = "SessionID";

		// Token: 0x040002E5 RID: 741
		internal const string ConnectionIDElement = "ConnectionID";

		// Token: 0x040002E6 RID: 742
		internal const string SPIDElement = "SPID";

		// Token: 0x040002E7 RID: 743
		internal const string DatabaseIDElement = "DatabaseID";

		// Token: 0x040002E8 RID: 744
		internal const string DatabaseNameElement = "DatabaseName";

		// Token: 0x040002E9 RID: 745
		internal const string AllowOverwrite = "AllowOverwrite";

		// Token: 0x040002EA RID: 746
		internal const string CancelAssociatedElement = "CancelAssociated";

		// Token: 0x040002EB RID: 747
		internal const string ExceptionElement = "Exception";

		// Token: 0x040002EC RID: 748
		internal const string MessagesElement = "Messages";

		// Token: 0x040002ED RID: 749
		internal const string CancelElement = "Cancel";

		// Token: 0x040002EE RID: 750
		internal const string HeaderElement = "Header";

		// Token: 0x040002EF RID: 751
		internal const string SessionElement = "Session";

		// Token: 0x040002F0 RID: 752
		internal const string BeginSessionElement = "BeginSession";

		// Token: 0x040002F1 RID: 753
		internal const string BeginGetSessionTokenElement = "BeginGetSessionToken";

		// Token: 0x040002F2 RID: 754
		internal const string SetAuthContextElement = "SetAuthContext";

		// Token: 0x040002F3 RID: 755
		internal const string ImageLoadElement = "ImageLoad";

		// Token: 0x040002F4 RID: 756
		internal const string ImageSaveElement = "ImageSave";

		// Token: 0x040002F5 RID: 757
		internal const string VersionElement = "Version";

		// Token: 0x040002F6 RID: 758
		internal const string EndSessionElement = "EndSession";

		// Token: 0x040002F7 RID: 759
		internal const string EnvelopeElement = "Envelope";

		// Token: 0x040002F8 RID: 760
		internal const string AuthenticateElement = "Authenticate";

		// Token: 0x040002F9 RID: 761
		internal const string AuthProtocolElement = "AuthProtocol";

		// Token: 0x040002FA RID: 762
		internal const string SspiHandshakeElement = "SspiHandshake";

		// Token: 0x040002FB RID: 763
		internal const string AuthTokenElement = "AuthToken";

		// Token: 0x040002FC RID: 764
		internal const string BodyElement = "Body";

		// Token: 0x040002FD RID: 765
		internal const string FaultElement = "Fault";

		// Token: 0x040002FE RID: 766
		internal const string ReturnElement = "return";

		// Token: 0x040002FF RID: 767
		internal const string ResultsElement = "results";

		// Token: 0x04000300 RID: 768
		internal const string RootElement = "root";

		// Token: 0x04000301 RID: 769
		internal const string FaultcodeElement = "faultcode";

		// Token: 0x04000302 RID: 770
		internal const string FaultstringElement = "faultstring";

		// Token: 0x04000303 RID: 771
		internal const string FaultactorElement = "faultactor";

		// Token: 0x04000304 RID: 772
		internal const string ErrorTypeElement = "errortype";

		// Token: 0x04000305 RID: 773
		internal const string IsPrimaryElement = "isprimary";

		// Token: 0x04000306 RID: 774
		internal const string DetailElement = "detail";

		// Token: 0x04000307 RID: 775
		internal const string SchemaElement = "schema";

		// Token: 0x04000308 RID: 776
		internal const string ErrorElement = "Error";

		// Token: 0x04000309 RID: 777
		internal const string ErrorLocationElement = "Location";

		// Token: 0x0400030A RID: 778
		internal const string StartElement = "Start";

		// Token: 0x0400030B RID: 779
		internal const string EndElement = "End";

		// Token: 0x0400030C RID: 780
		internal const string LineElement = "Line";

		// Token: 0x0400030D RID: 781
		internal const string ColumnElement = "Column";

		// Token: 0x0400030E RID: 782
		internal const string LineOffsetElement = "LineOffset";

		// Token: 0x0400030F RID: 783
		internal const string TextLengthElement = "TextLength";

		// Token: 0x04000310 RID: 784
		internal const string WarningElement = "Warning";

		// Token: 0x04000311 RID: 785
		internal const string RowNumber = "RowNumber";

		// Token: 0x04000312 RID: 786
		internal const string SourceObject = "SourceObject";

		// Token: 0x04000313 RID: 787
		internal const string DependsOnObject = "DependsOnObject";

		// Token: 0x04000314 RID: 788
		internal const string ErrorCallStack = "CallStack";

		// Token: 0x04000315 RID: 789
		internal const string Dimension = "Dimension";

		// Token: 0x04000316 RID: 790
		internal const string Hierarchy = "Hierarchy";

		// Token: 0x04000317 RID: 791
		internal const string Attribute = "Attribute";

		// Token: 0x04000318 RID: 792
		internal const string Cube = "Cube";

		// Token: 0x04000319 RID: 793
		internal const string MeasureGroup = "MeasureGroup";

		// Token: 0x0400031A RID: 794
		internal const string MemberName = "MemberName";

		// Token: 0x0400031B RID: 795
		internal const string TableName = "TableName";

		// Token: 0x0400031C RID: 796
		internal const string ColumnName = "ColumnName";

		// Token: 0x0400031D RID: 797
		internal const string PartitionName = "PartitionName";

		// Token: 0x0400031E RID: 798
		internal const string MeasureName = "MeasureName";

		// Token: 0x0400031F RID: 799
		internal const string CalculationItemName = "CalculationItemName";

		// Token: 0x04000320 RID: 800
		internal const string RoleName = "RoleName";

		// Token: 0x04000321 RID: 801
		internal const string ExecuteElement = "Execute";

		// Token: 0x04000322 RID: 802
		internal const string CommandElement = "Command";

		// Token: 0x04000323 RID: 803
		internal const string StatementElement = "Statement";

		// Token: 0x04000324 RID: 804
		internal const string PropertiesElement = "Properties";

		// Token: 0x04000325 RID: 805
		internal const string PropertyListElement = "PropertyList";

		// Token: 0x04000326 RID: 806
		internal const string ParametersElement = "Parameters";

		// Token: 0x04000327 RID: 807
		internal const string ParamElement = "Parameter";

		// Token: 0x04000328 RID: 808
		internal const string ParamNameElement = "Name";

		// Token: 0x04000329 RID: 809
		internal const string ParamValueElement = "Value";

		// Token: 0x0400032A RID: 810
		internal const string TokenElement = "Token";

		// Token: 0x0400032B RID: 811
		internal const string Role = "Role";

		// Token: 0x0400032C RID: 812
		internal const string SessionTokenElement = "SessionToken";

		// Token: 0x0400032D RID: 813
		internal const string AuthenticationSchemeElement = "AuthenticationScheme";

		// Token: 0x0400032E RID: 814
		internal const string AuthenticationSchemeDelegateToken = "DelegateToken";

		// Token: 0x0400032F RID: 815
		internal const string ScopeConnectionElement = "ScopeConnection";

		// Token: 0x04000330 RID: 816
		internal const string AccessTypeAttribute = "AccessType";

		// Token: 0x04000331 RID: 817
		internal const string AccessTypeReadOnly = "ReadOnly";

		// Token: 0x04000332 RID: 818
		internal const string AccessTypeDbAdmin = "DbAdmin";

		// Token: 0x04000333 RID: 819
		internal const string UserTokenElement = "UserToken";

		// Token: 0x04000334 RID: 820
		internal const string ExtAuthElement = "ExtAuth";

		// Token: 0x04000335 RID: 821
		internal const string ExtAuthInfoElement = "ExtAuthInfo";

		// Token: 0x04000336 RID: 822
		internal const string IdentityProviderElement = "IdentityProvider";

		// Token: 0x04000337 RID: 823
		internal const string BypassAuthorizationElement = "BypassAuthorization";

		// Token: 0x04000338 RID: 824
		internal const string RestrictCatalogElement = "RestrictCatalog";

		// Token: 0x04000339 RID: 825
		internal const string AccessModeElement = "AccessMode";

		// Token: 0x0400033A RID: 826
		internal const string UserNameElement = "UserName";

		// Token: 0x0400033B RID: 827
		internal const string RestrictRolesElement = "RestrictRoles";

		// Token: 0x0400033C RID: 828
		internal const string AuthenticateResponseElement = "AuthenticateResponse";

		// Token: 0x0400033D RID: 829
		internal const string ExecuteResponseElement = "ExecuteResponse";

		// Token: 0x0400033E RID: 830
		internal const string DiscoverResponseElement = "DiscoverResponse";

		// Token: 0x0400033F RID: 831
		internal const string OlapInfoElement = "OlapInfo";

		// Token: 0x04000340 RID: 832
		internal const string AxesInfoElement = "AxesInfo";

		// Token: 0x04000341 RID: 833
		internal const string AxisInfoElement = "AxisInfo";

		// Token: 0x04000342 RID: 834
		internal const string HierarchyInfoElement = "HierarchyInfo";

		// Token: 0x04000343 RID: 835
		internal const string CellInfoElement = "CellInfo";

		// Token: 0x04000344 RID: 836
		internal const string SlicerAxisName = "SlicerAxis";

		// Token: 0x04000345 RID: 837
		internal const string AxesElement = "Axes";

		// Token: 0x04000346 RID: 838
		internal const string AxisElement = "Axis";

		// Token: 0x04000347 RID: 839
		internal const string TuplesElement = "Tuples";

		// Token: 0x04000348 RID: 840
		internal const string TupleElement = "Tuple";

		// Token: 0x04000349 RID: 841
		internal const string CellDataElement = "CellData";

		// Token: 0x0400034A RID: 842
		internal const string CellElement = "Cell";

		// Token: 0x0400034B RID: 843
		internal const string MemberElement = "Member";

		// Token: 0x0400034C RID: 844
		internal const string CubeInfoElement = "CubeInfo";

		// Token: 0x0400034D RID: 845
		internal const string CubeNameElement = "CubeName";

		// Token: 0x0400034E RID: 846
		internal const string LastDataUpdateElement = "LastDataUpdate";

		// Token: 0x0400034F RID: 847
		internal const string CubeElement = "Cube";

		// Token: 0x04000350 RID: 848
		internal const string LastSchemaUpdateElement = "LastSchemaUpdate";

		// Token: 0x04000351 RID: 849
		internal const string HierarchyAttribute = "Hierarchy";

		// Token: 0x04000352 RID: 850
		internal const string CellOrdinalAttribute = "CellOrdinal";

		// Token: 0x04000353 RID: 851
		internal const string ValueProperty = "Value";

		// Token: 0x04000354 RID: 852
		internal const string DescriptionProperty = "Description";

		// Token: 0x04000355 RID: 853
		internal const string FmtValueProperty = "FmtValue";

		// Token: 0x04000356 RID: 854
		internal const string UniqueNameProperty = "UName";

		// Token: 0x04000357 RID: 855
		internal const string LevelNameProperty = "LName";

		// Token: 0x04000358 RID: 856
		internal const string LevelNumberProperty = "LNum";

		// Token: 0x04000359 RID: 857
		internal const string CaptionProperty = "Caption";

		// Token: 0x0400035A RID: 858
		internal const string ParentProperty = "Parent";

		// Token: 0x0400035B RID: 859
		internal const string DisplayInfoProperty = "DisplayInfo";

		// Token: 0x0400035C RID: 860
		internal const string NameAtribute = "name";

		// Token: 0x0400035D RID: 861
		internal const string GuidColumnType = "uuid";

		// Token: 0x0400035E RID: 862
		internal const string XmlDocumentColumnType = "xmlDocument";

		// Token: 0x0400035F RID: 863
		internal const string TypeAttributeName = "type";

		// Token: 0x04000360 RID: 864
		internal const string OccurenceUnbounded = "unbounded";

		// Token: 0x04000361 RID: 865
		internal const string FieldAttributeName = "field";

		// Token: 0x04000362 RID: 866
		internal const string NilAttributeName = "nil";

		// Token: 0x04000363 RID: 867
		internal const string TrueConstant = "true";

		// Token: 0x04000364 RID: 868
		internal const string DiscoverSoapAction = "SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Discover\"";

		// Token: 0x04000365 RID: 869
		internal const string ExecuteSoapAction = "SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"";

		// Token: 0x04000366 RID: 870
		internal const string DiscoverInstances = "DISCOVER_INSTANCES";

		// Token: 0x04000367 RID: 871
		internal const string InstanceNameRestriction = "INSTANCE_NAME";

		// Token: 0x04000368 RID: 872
		internal const string PortRestriction = "INSTANCE_PORT_NUMBER";

		// Token: 0x04000369 RID: 873
		internal const string DiscoverProperties = "DISCOVER_PROPERTIES";

		// Token: 0x0400036A RID: 874
		internal const string PropertyName = "PropertyName";

		// Token: 0x0400036B RID: 875
		internal const string ImpactAnalysisProperty = "ImpactAnalysis";

		// Token: 0x0400036C RID: 876
		internal const string DatabaseReadWriteModeElement = "ReadWriteMode";

		// Token: 0x0400036D RID: 877
		internal const string DatabaseReadWriteMode = "ReadWrite";

		// Token: 0x0400036E RID: 878
		internal const string DatabaseReadOnlyExclusiveMode = "ReadOnlyExclusive";

		// Token: 0x0400036F RID: 879
		internal const string AffectedObjectsElement = "AffectedObjects";

		// Token: 0x04000370 RID: 880
		internal const string BaseVersionAttribute = "BaseVersion";

		// Token: 0x04000371 RID: 881
		internal const string CurrentVersionAttribute = "CurrentVersion";
	}
}
