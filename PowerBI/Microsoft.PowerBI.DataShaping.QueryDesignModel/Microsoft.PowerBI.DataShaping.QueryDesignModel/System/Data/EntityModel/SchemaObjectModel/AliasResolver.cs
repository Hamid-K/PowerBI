using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200004E RID: 78
	internal sealed class AliasResolver
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x000112D4 File Offset: 0x0000F4D4
		public AliasResolver(Schema schema)
		{
			this._definingSchema = schema;
			if (!string.IsNullOrEmpty(schema.Alias))
			{
				this._aliasToNamespaceMap.Add(schema.Alias, schema.Namespace);
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00011330 File Offset: 0x0000F530
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

		// Token: 0x0600083A RID: 2106 RVA: 0x000113A3 File Offset: 0x0000F5A3
		public bool TryResolveAlias(string alias, out string namespaceName)
		{
			return this._aliasToNamespaceMap.TryGetValue(alias, out namespaceName);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000113B4 File Offset: 0x0000F5B4
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

		// Token: 0x0600083C RID: 2108 RVA: 0x00011430 File Offset: 0x0000F630
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

		// Token: 0x040006BA RID: 1722
		private Dictionary<string, string> _aliasToNamespaceMap = new Dictionary<string, string>(StringComparer.Ordinal);

		// Token: 0x040006BB RID: 1723
		private List<UsingElement> _usingElementCollection = new List<UsingElement>();

		// Token: 0x040006BC RID: 1724
		private Schema _definingSchema;

		// Token: 0x020002A2 RID: 674
		private enum NameKind
		{
			// Token: 0x04000F79 RID: 3961
			Alias,
			// Token: 0x04000F7A RID: 3962
			Namespace
		}
	}
}
