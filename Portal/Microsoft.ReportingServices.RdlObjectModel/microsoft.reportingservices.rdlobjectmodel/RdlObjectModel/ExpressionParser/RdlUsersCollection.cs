using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000228 RID: 552
	internal class RdlUsersCollection : ISimpleRdlCollection
	{
		// Token: 0x060012A1 RID: 4769 RVA: 0x00029BE2 File Offset: 0x00027DE2
		public IInternalExpression CreateReference()
		{
			return new FunctionUserCollection();
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00029BEC File Offset: 0x00027DEC
		public IInternalExpression CreateCollectionReference(IInternalExpression itemNameExpr)
		{
			if (itemNameExpr is ConstantString)
			{
				string constantValue = ((ConstantString)itemNameExpr).ConstantValue;
				Type type;
				if (!RdlUsersCollection.m_properties.TryGetValue(constantValue, out type))
				{
					RDLExceptionHelper.WriteInvalidCollectionItem(this.Name, constantValue);
				}
				return (IInternalExpression)Activator.CreateInstance(type);
			}
			return new FunctionUser(itemNameExpr);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00029C3C File Offset: 0x00027E3C
		public IInternalExpression CreatePropertyReference(string propertyName)
		{
			Type type;
			if (!RdlUsersCollection.m_propertiesIgnoreCase.TryGetValue(propertyName, out type))
			{
				RDLExceptionHelper.WriteInvalidCollectionItem(this.Name, propertyName);
			}
			return (IInternalExpression)Activator.CreateInstance(type);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00029C6F File Offset: 0x00027E6F
		public bool IsPredefinedCollectionProperty(string name)
		{
			return RdlUsersCollection.m_propertiesIgnoreCase.ContainsKey(name);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00029C7C File Offset: 0x00027E7C
		public bool IsPredefinedChildCollection(string name, out ISimpleRdlCollection childCollection)
		{
			childCollection = null;
			return false;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00029C82 File Offset: 0x00027E82
		public string Name
		{
			get
			{
				return "User";
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00029C8C File Offset: 0x00027E8C
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
					"UserID",
					typeof(FunctionUserID)
				},
				{
					"Language",
					typeof(FunctionUserLanguage)
				}
			};
		}

		// Token: 0x040005DD RID: 1501
		private static readonly Dictionary<string, Type> m_properties = RdlUsersCollection.InitProperties(false);

		// Token: 0x040005DE RID: 1502
		private static readonly Dictionary<string, Type> m_propertiesIgnoreCase = RdlUsersCollection.InitProperties(true);
	}
}
