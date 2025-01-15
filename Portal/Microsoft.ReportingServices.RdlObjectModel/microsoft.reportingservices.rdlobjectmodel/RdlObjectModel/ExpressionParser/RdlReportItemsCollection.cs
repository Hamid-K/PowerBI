using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000222 RID: 546
	internal class RdlReportItemsCollection : IComplexRdlCollection
	{
		// Token: 0x06001262 RID: 4706 RVA: 0x000296E7 File Offset: 0x000278E7
		public IInternalExpression CreateReference()
		{
			return new FunctionReportItemsCollection();
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000296EE File Offset: 0x000278EE
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
		{
			return new FunctionTextbox(itemNameExpr);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000296F6 File Offset: 0x000278F6
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
		{
			return new FunctionTextbox(itemNameExpr);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000296FE File Offset: 0x000278FE
		public bool IsPredefinedItemProperty(string name)
		{
			return RdlReportItemsCollection.m_properties.ContainsKey(name);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0002970B File Offset: 0x0002790B
		public bool IsPredefinedItemMethod(string name)
		{
			return false;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0002970E File Offset: 0x0002790E
		public bool IsPredefinedCollectionProperty(string name)
		{
			return false;
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00029711 File Offset: 0x00027911
		public string Name
		{
			get
			{
				return "ReportItems";
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00029718 File Offset: 0x00027918
		public string ItemName
		{
			get
			{
				return "ReportItem";
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0002971F File Offset: 0x0002791F
		private static Dictionary<string, Type> InitProperties()
		{
			return new Dictionary<string, Type>(StringUtil.CaseInsensitiveComparer) { 
			{
				"Value",
				typeof(object)
			} };
		}

		// Token: 0x040005D4 RID: 1492
		private static readonly Dictionary<string, Type> m_properties = RdlReportItemsCollection.InitProperties();
	}
}
