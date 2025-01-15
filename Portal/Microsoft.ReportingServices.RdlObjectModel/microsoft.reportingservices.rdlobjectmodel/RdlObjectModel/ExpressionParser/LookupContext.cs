using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel;
using Microsoft.SqlServer.Types;
using Microsoft.VisualBasic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200021D RID: 541
	internal abstract class LookupContext
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x00028EF8 File Offset: 0x000270F8
		internal LookupContext(string name)
		{
			this.m_name = name;
			this.m_types = new Dictionary<string, LookupContext>(StringUtil.CaseInsensitiveComparer);
			this.m_namespaces = new Dictionary<string, LookupContext>(StringUtil.CaseInsensitiveComparer);
			this.m_members = new Dictionary<string, MemberContext>(StringUtil.CaseInsensitiveComparer);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00028F38 File Offset: 0x00027138
		protected void InitEnvironment(IEnvironmentFilter filter)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringUtil.CaseInsensitiveComparer);
			dictionary["System.Int32"] = "Integer";
			dictionary["System.Int64"] = "Long";
			dictionary["System.Int16"] = "Short";
			dictionary["System.UInt32"] = "UInteger";
			dictionary["System.UInt64"] = "ULong";
			dictionary["System.UInt16"] = "UShort";
			dictionary["System.DateTime"] = "Date";
			this.ProcessAssembly(Assembly.GetAssembly(typeof(DateAndTime)), filter, "Microsoft.VisualBasic", null, Array.Empty<string>());
			this.ProcessAssembly(Assembly.GetAssembly(typeof(string)), filter, "System", dictionary, new string[] { "System.Convert", "System.Math" });
			this.ProcessAssembly(Assembly.GetAssembly(typeof(Uri)), filter, "System", null, Array.Empty<string>());
			this.ProcessAssembly(Assembly.GetAssembly(typeof(SqlGeography)), filter, "Microsoft.SqlServer.Types", null, Array.Empty<string>());
			this.m_namespaces.Add("Global", this);
			TypeContext typeContext = new TypeContext("VBFunctions", typeof(VBFunctions), false, false, DefaultEnvironmentFilter.Instance, BindingFlags.NonPublic);
			this.MergeMembers(typeContext);
			MemberContext memberContext = new MemberContext("Code", MemberContext.MemberContextTypes.Property);
			this.m_members.Add(memberContext.Name, memberContext);
			TypeContext typeContext2 = new TypeContext("ReportItem", typeof(ReportItem), false, false, DefaultEnvironmentFilter.Instance, BindingFlags.Instance | BindingFlags.NonPublic);
			this.m_types.Add("Me", typeContext2);
			this.MergeMembers(typeContext2);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000290E0 File Offset: 0x000272E0
		protected void ProcessAssembly(Assembly assembly, IEnvironmentFilter filter, string importedNs, Dictionary<string, string> specialAliasTable, params string[] importedTypes)
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>(StringUtil.CaseInsensitiveComparer);
			foreach (string text in importedTypes)
			{
				dictionary[text] = true;
			}
			string[] array = this.SplitNamespace(importedNs);
			foreach (Type type in assembly.GetTypes())
			{
				bool flag;
				bool flag2;
				if (!type.IsNested && type.IsPublic && filter.IsAllowedType(type, out flag, out flag2))
				{
					string @namespace = type.Namespace;
					string[] array2 = this.SplitNamespace(@namespace);
					bool flag3 = !string.IsNullOrEmpty(importedNs);
					LookupContext lookupContext = this;
					for (int j = 0; j < array2.Length; j++)
					{
						string text2 = array2[j];
						LookupContext lookupContext2;
						if (!lookupContext.m_namespaces.TryGetValue(text2, out lookupContext2))
						{
							lookupContext2 = new NamespaceContext(text2);
							lookupContext.m_namespaces.Add(text2, lookupContext2);
						}
						lookupContext = lookupContext2;
						if (flag3)
						{
							if (j == array.Length)
							{
								if (!this.m_namespaces.ContainsKey(text2))
								{
									this.m_namespaces.Add(text2, lookupContext2);
								}
								flag3 = false;
							}
							else
							{
								flag3 = StringUtil.EqualsIgnoreCase(text2, array[j]);
							}
						}
					}
					TypeContext typeContext = lookupContext.AddType(type, flag, flag2, filter);
					if (StringUtil.EqualsIgnoreCase(@namespace, importedNs))
					{
						if (!this.m_types.ContainsKey(type.Name))
						{
							this.m_types.Add(type.Name, typeContext);
						}
						if (typeContext.IsStandardModule())
						{
							this.MergeMembers(typeContext);
						}
					}
					if (dictionary.ContainsKey(type.FullName))
					{
						this.MergeMembers(typeContext);
					}
					string text3;
					if (specialAliasTable != null && specialAliasTable.TryGetValue(type.FullName, out text3))
					{
						this.m_types.Add(text3, typeContext);
					}
				}
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000292A0 File Offset: 0x000274A0
		private void MergeMembers(TypeContext source)
		{
			foreach (KeyValuePair<string, MemberContext> keyValuePair in source.m_members)
			{
				this.m_members[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00029308 File Offset: 0x00027508
		protected TypeContext AddType(Type type, bool allowNew, bool allowNewArray, IEnvironmentFilter filter)
		{
			string name = type.Name;
			LookupContext lookupContext;
			if (!this.m_types.TryGetValue(name, out lookupContext))
			{
				lookupContext = new TypeContext(name, type, allowNew, allowNewArray, filter);
				this.m_types.Add(name, lookupContext);
			}
			foreach (Type type2 in type.GetNestedTypes())
			{
				bool flag;
				bool flag2;
				if (filter.IsAllowedType(type2, out flag, out flag2))
				{
					lookupContext.AddType(type2, flag, flag2, filter);
				}
			}
			return (TypeContext)lookupContext;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00029383 File Offset: 0x00027583
		private string[] SplitNamespace(string nsStr)
		{
			if (string.IsNullOrEmpty(nsStr))
			{
				return new string[0];
			}
			return nsStr.Split(new char[] { '.' });
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000293A5 File Offset: 0x000275A5
		internal virtual bool TryMatchMember(string identifier, out MemberContext member)
		{
			return this.m_members.TryGetValue(identifier, out member);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000293B4 File Offset: 0x000275B4
		internal virtual bool TryMatchSubContext(string identifier, out LookupContext subContext)
		{
			if (this.m_namespaces.TryGetValue(identifier, out subContext))
			{
				return true;
			}
			if (this.m_types.TryGetValue(identifier, out subContext))
			{
				return true;
			}
			subContext = null;
			return false;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x000293DC File Offset: 0x000275DC
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x040005CA RID: 1482
		private readonly string m_name;

		// Token: 0x040005CB RID: 1483
		protected Dictionary<string, LookupContext> m_namespaces;

		// Token: 0x040005CC RID: 1484
		protected Dictionary<string, LookupContext> m_types;

		// Token: 0x040005CD RID: 1485
		protected Dictionary<string, MemberContext> m_members;
	}
}
