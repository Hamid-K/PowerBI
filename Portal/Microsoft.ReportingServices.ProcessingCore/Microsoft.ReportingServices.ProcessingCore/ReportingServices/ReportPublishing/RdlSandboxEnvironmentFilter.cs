using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000392 RID: 914
	internal class RdlSandboxEnvironmentFilter : IEnvironmentFilter
	{
		// Token: 0x06002534 RID: 9524 RVA: 0x000B1E74 File Offset: 0x000B0074
		internal RdlSandboxEnvironmentFilter(IRdlSandboxConfig sandboxConfig)
		{
			this.Init(sandboxConfig);
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000B1E84 File Offset: 0x000B0084
		private void Init(IRdlSandboxConfig sandboxConfig)
		{
			this.m_wildcardedNamespaces = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			this.m_namespaceToTypes = new Dictionary<string, RdlSandboxEnvironmentFilter.TypeLookup>(StringComparer.OrdinalIgnoreCase);
			this.m_deniedMembers = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			if (sandboxConfig.AllowedTypes != null)
			{
				foreach (IRdlSandboxTypeInfo rdlSandboxTypeInfo in sandboxConfig.AllowedTypes)
				{
					if (string.Equals(rdlSandboxTypeInfo.Name, "*", StringComparison.Ordinal))
					{
						this.m_wildcardedNamespaces[rdlSandboxTypeInfo.Namespace] = rdlSandboxTypeInfo.AllowNew;
					}
					else
					{
						RdlSandboxEnvironmentFilter.TypeLookup typeLookup;
						if (!this.m_namespaceToTypes.TryGetValue(rdlSandboxTypeInfo.Namespace, out typeLookup))
						{
							typeLookup = new RdlSandboxEnvironmentFilter.TypeLookup();
							this.m_namespaceToTypes.Add(rdlSandboxTypeInfo.Namespace, typeLookup);
						}
						typeLookup[rdlSandboxTypeInfo.Name] = rdlSandboxTypeInfo.AllowNew;
					}
				}
			}
			if (sandboxConfig.DeniedMembers != null)
			{
				foreach (string text in sandboxConfig.DeniedMembers)
				{
					this.m_deniedMembers[text] = true;
				}
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000B1FBC File Offset: 0x000B01BC
		public bool IsAllowedMember(string memberName)
		{
			return !this.m_deniedMembers.ContainsKey(memberName);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000B1FD0 File Offset: 0x000B01D0
		public bool IsAllowedType(Type type, out bool allowNew, out bool allowNewArray)
		{
			allowNewArray = false;
			if ((!type.IsPublic && !type.IsNestedPublic) || type.IsGenericType)
			{
				allowNew = false;
				return false;
			}
			if (this.m_wildcardedNamespaces.TryGetValue(type.Namespace, out allowNew))
			{
				return true;
			}
			RdlSandboxEnvironmentFilter.TypeLookup typeLookup;
			if (this.m_namespaceToTypes.TryGetValue(type.Namespace, out typeLookup))
			{
				string text;
				if (type.IsNested)
				{
					text = type.FullName;
					text = text.Substring(type.Namespace.Length + 1);
					text = text.Replace('+', '.');
				}
				else
				{
					text = type.Name;
				}
				if (typeLookup.TryGetValue(text, out allowNew))
				{
					return true;
				}
			}
			allowNew = false;
			return false;
		}

		// Token: 0x040015CB RID: 5579
		private Dictionary<string, bool> m_wildcardedNamespaces;

		// Token: 0x040015CC RID: 5580
		private Dictionary<string, RdlSandboxEnvironmentFilter.TypeLookup> m_namespaceToTypes;

		// Token: 0x040015CD RID: 5581
		private Dictionary<string, bool> m_deniedMembers;

		// Token: 0x040015CE RID: 5582
		private const string TypeNameWildcard = "*";

		// Token: 0x02000959 RID: 2393
		private class TypeLookup : Dictionary<string, bool>
		{
			// Token: 0x06008006 RID: 32774 RVA: 0x002106D3 File Offset: 0x0020E8D3
			public TypeLookup()
				: base(StringComparer.OrdinalIgnoreCase)
			{
			}
		}
	}
}
