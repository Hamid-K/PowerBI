using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.AnalysisServices.Tabular.DDL
{
	// Token: 0x02000125 RID: 293
	internal static class XmlaConstants
	{
		// Token: 0x06001476 RID: 5238 RVA: 0x0008AE44 File Offset: 0x00089044
		static XmlaConstants()
		{
			XmlaConstants.xmlTypes.Add(typeof(string), "xsd:string");
			XmlaConstants.xmlTypes.Add(typeof(bool), "xsd:boolean");
			XmlaConstants.xmlTypes.Add(typeof(decimal), "xsd:decimal");
			XmlaConstants.xmlTypes.Add(typeof(float), "xsd:float");
			XmlaConstants.xmlTypes.Add(typeof(double), "xsd:double");
			XmlaConstants.xmlTypes.Add(typeof(sbyte), "xsd:byte");
			XmlaConstants.xmlTypes.Add(typeof(byte), "xsd:unsignedByte");
			XmlaConstants.xmlTypes.Add(typeof(short), "xsd:short");
			XmlaConstants.xmlTypes.Add(typeof(ushort), "xsd:unsignedShort");
			XmlaConstants.xmlTypes.Add(typeof(int), "xsd:int");
			XmlaConstants.xmlTypes.Add(typeof(uint), "xsd:unsignedInt");
			XmlaConstants.xmlTypes.Add(typeof(long), "xsd:long");
			XmlaConstants.xmlTypes.Add(typeof(ulong), "xsd:unsignedLong");
			XmlaConstants.xmlTypes.Add(typeof(DateTime), "xsd:dateTime");
			XmlaConstants.xmlTypes.Add(typeof(Guid), "uuid");
			XmlaConstants.xmlTypes.Add(typeof(byte[]), "xsd:base64Binary");
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0008AFEC File Offset: 0x000891EC
		internal static string GetXmlType(Type type)
		{
			string text;
			XmlaConstants.xmlTypes.TryGetValue(type, out text);
			return text;
		}

		// Token: 0x04000309 RID: 777
		internal const string XsdString = "xsd:string";

		// Token: 0x0400030A RID: 778
		internal const string XsdBoolean = "xsd:boolean";

		// Token: 0x0400030B RID: 779
		internal const string XsdDecimal = "xsd:decimal";

		// Token: 0x0400030C RID: 780
		internal const string XsdFloat = "xsd:float";

		// Token: 0x0400030D RID: 781
		internal const string XsdDouble = "xsd:double";

		// Token: 0x0400030E RID: 782
		internal const string XsdByte = "xsd:byte";

		// Token: 0x0400030F RID: 783
		internal const string XsdUnsignedByte = "xsd:unsignedByte";

		// Token: 0x04000310 RID: 784
		internal const string XsdShort = "xsd:short";

		// Token: 0x04000311 RID: 785
		internal const string XsdUnsignedShort = "xsd:unsignedShort";

		// Token: 0x04000312 RID: 786
		internal const string XsdInt = "xsd:int";

		// Token: 0x04000313 RID: 787
		internal const string XsdUnsignedInt = "xsd:unsignedInt";

		// Token: 0x04000314 RID: 788
		internal const string XsdLong = "xsd:long";

		// Token: 0x04000315 RID: 789
		internal const string XsdUnsignedLong = "xsd:unsignedLong";

		// Token: 0x04000316 RID: 790
		internal const string XsdDateTime = "xsd:dateTime";

		// Token: 0x04000317 RID: 791
		internal const string XsdBase64Binary = "xsd:base64Binary";

		// Token: 0x04000318 RID: 792
		internal const string XmlnsPrefix = "xmlns";

		// Token: 0x04000319 RID: 793
		internal const string XsdPrefix = "xsd";

		// Token: 0x0400031A RID: 794
		internal const string XsiPrefix = "xsi";

		// Token: 0x0400031B RID: 795
		internal const string SqlPrefix = "sql";

		// Token: 0x0400031C RID: 796
		internal const string EnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x0400031D RID: 797
		internal const string XmlaNamespace = "urn:schemas-microsoft-com:xml-analysis";

		// Token: 0x0400031E RID: 798
		internal const string RowsetNamespace = "urn:schemas-microsoft-com:xml-analysis:rowset";

		// Token: 0x0400031F RID: 799
		internal const string ReturnAffectedObjectsProp = "ReturnAffectedObjects";

		// Token: 0x04000320 RID: 800
		internal const string CommitTransaction = "CommitTransaction";

		// Token: 0x04000321 RID: 801
		private static readonly Dictionary<Type, string> xmlTypes = new Dictionary<Type, string>();

		// Token: 0x02000318 RID: 792
		internal static class NS
		{
			// Token: 0x04000D7D RID: 3453
			public const string xsd = "http://www.w3.org/2001/XMLSchema";

			// Token: 0x04000D7E RID: 3454
			public const string xsi = "http://www.w3.org/2001/XMLSchema-instance";

			// Token: 0x04000D7F RID: 3455
			public const string sql = "urn:schemas-microsoft-com:xml-sql";

			// Token: 0x04000D80 RID: 3456
			public const string ana = "urn:schemas-microsoft-com:xml-analysis";

			// Token: 0x04000D81 RID: 3457
			public const string rst = "urn:schemas-microsoft-com:xml-analysis:rowset";

			// Token: 0x04000D82 RID: 3458
			public const string empty = "urn:schemas-microsoft-com:xml-analysis:empty";

			// Token: 0x04000D83 RID: 3459
			public const string exc = "urn:schemas-microsoft-com:xml-analysis:exception";

			// Token: 0x04000D84 RID: 3460
			public const string soap = "http://schemas.xmlsoap.org/soap/envelope/";

			// Token: 0x04000D85 RID: 3461
			public const string mrs = "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults";

			// Token: 0x04000D86 RID: 3462
			public const string ddl = "http://schemas.microsoft.com/analysisservices/2003/engine";

			// Token: 0x04000D87 RID: 3463
			public const string ddl2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2";

			// Token: 0x04000D88 RID: 3464
			public const string ddl2_2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2/2";

			// Token: 0x04000D89 RID: 3465
			public const string ddl100_100 = "http://schemas.microsoft.com/analysisservices/2008/engine/100/100";

			// Token: 0x04000D8A RID: 3466
			public const string ddl200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200";

			// Token: 0x04000D8B RID: 3467
			public const string ddl200_200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200/200";

			// Token: 0x04000D8C RID: 3468
			public const string ddl300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300";

			// Token: 0x04000D8D RID: 3469
			public const string ddl300_300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300/300";

			// Token: 0x04000D8E RID: 3470
			public const string ddl400 = "http://schemas.microsoft.com/analysisservices/2012/engine/400";

			// Token: 0x04000D8F RID: 3471
			public const string ddl400_400 = "http://schemas.microsoft.com/analysisservices/2012/engine/400/400";

			// Token: 0x04000D90 RID: 3472
			public const string ddl500 = "http://schemas.microsoft.com/analysisservices/2013/engine/500";

			// Token: 0x04000D91 RID: 3473
			public const string ddl500_500 = "http://schemas.microsoft.com/analysisservices/2013/engine/500/500";

			// Token: 0x04000D92 RID: 3474
			public const string ddl921 = "http://schemas.microsoft.com/analysisservices/2021/engine/921";

			// Token: 0x04000D93 RID: 3475
			public const string ddl921_921 = "http://schemas.microsoft.com/analysisservices/2021/engine/921/921";

			// Token: 0x04000D94 RID: 3476
			public const string tmddl = "http://schemas.microsoft.com/analysisservices/2014/engine";
		}

		// Token: 0x02000319 RID: 793
		internal static class XNS
		{
			// Token: 0x04000D95 RID: 3477
			public static XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

			// Token: 0x04000D96 RID: 3478
			public static XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

			// Token: 0x04000D97 RID: 3479
			public static XNamespace sql = "urn:schemas-microsoft-com:xml-sql";

			// Token: 0x04000D98 RID: 3480
			public static XNamespace ana = "urn:schemas-microsoft-com:xml-analysis";

			// Token: 0x04000D99 RID: 3481
			public static XNamespace rst = "urn:schemas-microsoft-com:xml-analysis:rowset";

			// Token: 0x04000D9A RID: 3482
			public static XNamespace empty = "urn:schemas-microsoft-com:xml-analysis:empty";

			// Token: 0x04000D9B RID: 3483
			public static XNamespace exc = "urn:schemas-microsoft-com:xml-analysis:exception";

			// Token: 0x04000D9C RID: 3484
			public static XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";

			// Token: 0x04000D9D RID: 3485
			public static XNamespace mrs = "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults";

			// Token: 0x04000D9E RID: 3486
			public static XNamespace ddl = "http://schemas.microsoft.com/analysisservices/2003/engine";

			// Token: 0x04000D9F RID: 3487
			public static XNamespace ddl2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2";

			// Token: 0x04000DA0 RID: 3488
			public static XNamespace ddl2_2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2/2";

			// Token: 0x04000DA1 RID: 3489
			public static XNamespace ddl100_100 = "http://schemas.microsoft.com/analysisservices/2008/engine/100/100";

			// Token: 0x04000DA2 RID: 3490
			public static XNamespace ddl200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200";

			// Token: 0x04000DA3 RID: 3491
			public static XNamespace ddl200_200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200/200";

			// Token: 0x04000DA4 RID: 3492
			public static XNamespace ddl300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300";

			// Token: 0x04000DA5 RID: 3493
			public static XNamespace ddl300_300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300/300";

			// Token: 0x04000DA6 RID: 3494
			public static XNamespace ddl400 = "http://schemas.microsoft.com/analysisservices/2012/engine/400";

			// Token: 0x04000DA7 RID: 3495
			public static XNamespace ddl400_400 = "http://schemas.microsoft.com/analysisservices/2012/engine/400/400";

			// Token: 0x04000DA8 RID: 3496
			public static XNamespace ddl500 = "http://schemas.microsoft.com/analysisservices/2013/engine/500";

			// Token: 0x04000DA9 RID: 3497
			public static XNamespace ddl500_500 = "http://schemas.microsoft.com/analysisservices/2013/engine/500/500";

			// Token: 0x04000DAA RID: 3498
			public static XNamespace tmddl = "http://schemas.microsoft.com/analysisservices/2014/engine";
		}

		// Token: 0x0200031A RID: 794
		internal static class DDLConstants
		{
			// Token: 0x04000DAB RID: 3499
			public static XName Create = XmlaConstants.XNS.tmddl + "Create";

			// Token: 0x04000DAC RID: 3500
			public static XName Alter = XmlaConstants.XNS.tmddl + "Alter";

			// Token: 0x04000DAD RID: 3501
			public static XName Delete = XmlaConstants.XNS.tmddl + "Delete";

			// Token: 0x04000DAE RID: 3502
			public static XName Refresh = XmlaConstants.XNS.tmddl + "Refresh";

			// Token: 0x04000DAF RID: 3503
			public static XName Rename = XmlaConstants.XNS.tmddl + "Rename";

			// Token: 0x04000DB0 RID: 3504
			public static XName SequencePoint = XmlaConstants.XNS.tmddl + "SequencePoint";

			// Token: 0x04000DB1 RID: 3505
			public static XName Upgrade = XmlaConstants.XNS.tmddl + "Upgrade";

			// Token: 0x04000DB2 RID: 3506
			public static XName MergePartitions = XmlaConstants.XNS.tmddl + "MergePartitions";

			// Token: 0x04000DB3 RID: 3507
			public static XName AnalyzeRefreshPolicyImpact = XmlaConstants.XNS.tmddl + "AnalyzeRefreshPolicyImpact";
		}
	}
}
