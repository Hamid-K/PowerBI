using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000226 RID: 550
	internal class RdlGlobalsCollection : ISimpleRdlCollection
	{
		// Token: 0x0600128F RID: 4751 RVA: 0x0002996A File Offset: 0x00027B6A
		public IInternalExpression CreateReference()
		{
			return new FunctionGlobalsCollection();
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00029974 File Offset: 0x00027B74
		public IInternalExpression CreateCollectionReference(IInternalExpression itemNameExpr)
		{
			if (itemNameExpr is ConstantString)
			{
				string constantValue = ((ConstantString)itemNameExpr).ConstantValue;
				Type type;
				if (!RdlGlobalsCollection.m_properties.TryGetValue(constantValue, out type))
				{
					RDLExceptionHelper.WriteInvalidCollectionItem(this.Name, constantValue);
				}
				return (IInternalExpression)Activator.CreateInstance(type);
			}
			return new FunctionGlobal(itemNameExpr);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000299C4 File Offset: 0x00027BC4
		public IInternalExpression CreatePropertyReference(string propertyName)
		{
			Type type;
			if (!RdlGlobalsCollection.m_propertiesIgnoreCase.TryGetValue(propertyName, out type))
			{
				RDLExceptionHelper.WriteInvalidCollectionItem(this.Name, propertyName);
			}
			return (IInternalExpression)Activator.CreateInstance(type);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000299F7 File Offset: 0x00027BF7
		public bool IsPredefinedCollectionProperty(string name)
		{
			return RdlGlobalsCollection.m_propertiesIgnoreCase.ContainsKey(name);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00029A04 File Offset: 0x00027C04
		public bool IsPredefinedChildCollection(string name, out ISimpleRdlCollection childCollection)
		{
			if (string.Equals(name, "RenderFormat", StringComparison.OrdinalIgnoreCase))
			{
				childCollection = RdlGlobalsCollection.m_renderFormatCollection;
				return true;
			}
			childCollection = null;
			return false;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00029A21 File Offset: 0x00027C21
		public string Name
		{
			get
			{
				return "Globals";
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00029A28 File Offset: 0x00027C28
		private static Dictionary<string, Type> InitProperties(bool ignoreCase)
		{
			StringComparer stringComparer;
			if (ignoreCase)
			{
				stringComparer = StringUtil.CaseInsensitiveComparer;
			}
			else
			{
				stringComparer = StringUtil.CaseSensitiveComparer;
			}
			return new Dictionary<string, Type>(stringComparer)
			{
				{
					"PageNumber",
					typeof(FunctionPageNumber)
				},
				{
					"TotalPages",
					typeof(FunctionTotalPages)
				},
				{
					"ExecutionTime",
					typeof(FunctionExecutionTime)
				},
				{
					"ReportServerUrl",
					typeof(FunctionReportServerUrl)
				},
				{
					"ReportFolder",
					typeof(FunctionReportFolder)
				},
				{
					"ReportName",
					typeof(FunctionReportName)
				},
				{
					"RenderFormat",
					typeof(FunctionRenderFormat)
				},
				{
					"PageName",
					typeof(FunctionPageName)
				},
				{
					"OverallPageNumber",
					typeof(FunctionOverallPageNumber)
				},
				{
					"OverallTotalPages",
					typeof(FunctionOverallTotalPages)
				}
			};
		}

		// Token: 0x040005D9 RID: 1497
		private static readonly Dictionary<string, Type> m_properties = RdlGlobalsCollection.InitProperties(false);

		// Token: 0x040005DA RID: 1498
		private static readonly Dictionary<string, Type> m_propertiesIgnoreCase = RdlGlobalsCollection.InitProperties(true);

		// Token: 0x040005DB RID: 1499
		private static readonly ISimpleRdlCollection m_renderFormatCollection = new RdlRenderFormatCollection();
	}
}
