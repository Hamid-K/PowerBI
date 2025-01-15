using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000227 RID: 551
	internal class RdlRenderFormatCollection : ISimpleRdlCollection
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x00029B48 File Offset: 0x00027D48
		public IInternalExpression CreateReference()
		{
			return new FunctionRenderFormat();
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00029B4F File Offset: 0x00027D4F
		public IInternalExpression CreateCollectionReference(IInternalExpression itemNameExpr)
		{
			RDLExceptionHelper.WriteInvalidCollectionItem(this.Name, itemNameExpr.WriteSource());
			return null;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00029B63 File Offset: 0x00027D63
		public IInternalExpression CreatePropertyReference(string propertyName)
		{
			if (!RdlRenderFormatCollection.m_propertiesIgnoreCase.ContainsKey(propertyName))
			{
				RDLExceptionHelper.WriteInvalidCollectionItem(this.Name, propertyName);
			}
			return new FunctionRenderFormat(propertyName);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00029B84 File Offset: 0x00027D84
		public bool IsPredefinedCollectionProperty(string name)
		{
			return RdlRenderFormatCollection.m_propertiesIgnoreCase.ContainsKey(name);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00029B91 File Offset: 0x00027D91
		public bool IsPredefinedChildCollection(string name, out ISimpleRdlCollection childCollection)
		{
			childCollection = null;
			return false;
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00029B97 File Offset: 0x00027D97
		public string Name
		{
			get
			{
				return "RenderFormat";
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00029B9E File Offset: 0x00027D9E
		private static Dictionary<string, bool> InitProperties()
		{
			return new Dictionary<string, bool>(StringUtil.CaseInsensitiveComparer)
			{
				{ "Name", true },
				{ "IsInteractive", true },
				{ "DeviceInfo", true }
			};
		}

		// Token: 0x040005DC RID: 1500
		private static readonly Dictionary<string, bool> m_propertiesIgnoreCase = RdlRenderFormatCollection.InitProperties();
	}
}
