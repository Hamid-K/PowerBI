using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000318 RID: 792
	internal sealed class AliasResolver
	{
		// Token: 0x060025C2 RID: 9666 RVA: 0x0006BCC0 File Offset: 0x00069EC0
		public AliasResolver(Schema schema)
		{
			this._definingSchema = schema;
			if (!string.IsNullOrEmpty(schema.Alias))
			{
				this._aliasToNamespaceMap.Add(schema.Alias, schema.Namespace);
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0006BD1C File Offset: 0x00069F1C
		public void Add(UsingElement usingElement)
		{
			string text = usingElement.NamespaceName;
			string text2 = usingElement.Alias;
			if (this.CheckForSystemNamespace(usingElement, text2, AliasResolver.NameKind.Alias))
			{
				text2 = null;
			}
			if (this.CheckForSystemNamespace(usingElement, text, AliasResolver.NameKind.Namespace))
			{
				text = null;
			}
			if (text2 != null && this._aliasToNamespaceMap.ContainsKey(text2))
			{
				usingElement.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.AliasNameIsAlreadyDefined(text2));
				text2 = null;
			}
			if (text2 != null)
			{
				this._aliasToNamespaceMap.Add(text2, text);
				this._usingElementCollection.Add(usingElement);
			}
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0006BD8F File Offset: 0x00069F8F
		public bool TryResolveAlias(string alias, out string namespaceName)
		{
			return this._aliasToNamespaceMap.TryGetValue(alias, out namespaceName);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0006BDA0 File Offset: 0x00069FA0
		public void ResolveNamespaces()
		{
			foreach (UsingElement usingElement in this._usingElementCollection)
			{
				if (!this._definingSchema.SchemaManager.IsValidNamespaceName(usingElement.NamespaceName))
				{
					usingElement.AddError(ErrorCode.InvalidNamespaceInUsing, EdmSchemaErrorSeverity.Error, Strings.InvalidNamespaceInUsing(usingElement.NamespaceName));
				}
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0006BE1C File Offset: 0x0006A01C
		private bool CheckForSystemNamespace(UsingElement refSchema, string name, AliasResolver.NameKind nameKind)
		{
			if (EdmItemCollection.IsSystemNamespace(this._definingSchema.ProviderManifest, name))
			{
				if (nameKind == AliasResolver.NameKind.Alias)
				{
					refSchema.AddError(ErrorCode.CannotUseSystemNamespaceAsAlias, EdmSchemaErrorSeverity.Error, Strings.CannotUseSystemNamespaceAsAlias(name));
				}
				else
				{
					refSchema.AddError(ErrorCode.NeedNotUseSystemNamespaceInUsing, EdmSchemaErrorSeverity.Error, Strings.NeedNotUseSystemNamespaceInUsing(name));
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000D46 RID: 3398
		private readonly Dictionary<string, string> _aliasToNamespaceMap = new Dictionary<string, string>(StringComparer.Ordinal);

		// Token: 0x04000D47 RID: 3399
		private readonly List<UsingElement> _usingElementCollection = new List<UsingElement>();

		// Token: 0x04000D48 RID: 3400
		private readonly Schema _definingSchema;

		// Token: 0x020009BC RID: 2492
		private enum NameKind
		{
			// Token: 0x04002805 RID: 10245
			Alias,
			// Token: 0x04002806 RID: 10246
			Namespace
		}
	}
}
