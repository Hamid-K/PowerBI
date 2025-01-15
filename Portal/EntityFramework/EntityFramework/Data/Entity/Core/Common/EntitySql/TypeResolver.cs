using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200066F RID: 1647
	internal sealed class TypeResolver
	{
		// Token: 0x06004ED6 RID: 20182 RVA: 0x0011EF58 File Offset: 0x0011D158
		internal TypeResolver(Perspective perspective, ParserOptions parserOptions)
		{
			this._perspective = perspective;
			this._parserOptions = parserOptions;
			this._aliasedNamespaces = new Dictionary<string, MetadataNamespace>(parserOptions.NameComparer);
			this._namespaces = new HashSet<MetadataNamespace>(MetadataMember.CreateMetadataMemberNameEqualityComparer(parserOptions.NameComparer));
			this._functionDefinitions = new Dictionary<string, List<InlineFunctionInfo>>(parserOptions.NameComparer);
			this._includeInlineFunctions = true;
			this._resolveLeftMostUnqualifiedNameAsNamespaceOnly = false;
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x0011EFBF File Offset: 0x0011D1BF
		internal Perspective Perspective
		{
			get
			{
				return this._perspective;
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x0011EFC7 File Offset: 0x0011D1C7
		internal ICollection<MetadataNamespace> NamespaceImports
		{
			get
			{
				return this._namespaces;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x0011EFCF File Offset: 0x0011D1CF
		internal static TypeUsage StringType
		{
			get
			{
				return MetadataWorkspace.GetCanonicalModelTypeUsage(PrimitiveTypeKind.String);
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06004EDA RID: 20186 RVA: 0x0011EFD8 File Offset: 0x0011D1D8
		internal static TypeUsage BooleanType
		{
			get
			{
				return MetadataWorkspace.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Boolean);
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x0011EFE0 File Offset: 0x0011D1E0
		internal static TypeUsage Int64Type
		{
			get
			{
				return MetadataWorkspace.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int64);
			}
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x0011EFEC File Offset: 0x0011D1EC
		internal void AddAliasedNamespaceImport(string alias, MetadataNamespace @namespace, ErrorContext errCtx)
		{
			if (this._aliasedNamespaces.ContainsKey(alias))
			{
				string text = Strings.NamespaceAliasAlreadyUsed(alias);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			this._aliasedNamespaces.Add(alias, @namespace);
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x0011F024 File Offset: 0x0011D224
		internal void AddNamespaceImport(MetadataNamespace @namespace, ErrorContext errCtx)
		{
			if (this._namespaces.Contains(@namespace))
			{
				string text = Strings.NamespaceAlreadyImported(@namespace.Name);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			this._namespaces.Add(@namespace);
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x0011F064 File Offset: 0x0011D264
		internal void DeclareInlineFunction(string name, InlineFunctionInfo functionInfo)
		{
			List<InlineFunctionInfo> list;
			if (!this._functionDefinitions.TryGetValue(name, out list))
			{
				list = new List<InlineFunctionInfo>();
				this._functionDefinitions.Add(name, list);
			}
			if (list.Exists((InlineFunctionInfo overload) => overload.Parameters.Select((DbVariableReferenceExpression p) => p.ResultType).SequenceEqual(functionInfo.Parameters.Select((DbVariableReferenceExpression p) => p.ResultType), TypeResolver.TypeUsageStructuralComparer.Instance)))
			{
				ErrorContext errCtx = functionInfo.FunctionDefAst.ErrCtx;
				string text = Strings.DuplicatedInlineFunctionOverload(name);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			list.Add(functionInfo);
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x0011F0E0 File Offset: 0x0011D2E0
		internal IDisposable EnterFunctionNameResolution(bool includeInlineFunctions)
		{
			bool savedIncludeInlineFunctions = this._includeInlineFunctions;
			this._includeInlineFunctions = includeInlineFunctions;
			return new Disposer(delegate
			{
				this._includeInlineFunctions = savedIncludeInlineFunctions;
			});
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x0011F111 File Offset: 0x0011D311
		internal IDisposable EnterBackwardCompatibilityResolution()
		{
			this._resolveLeftMostUnqualifiedNameAsNamespaceOnly = true;
			return new Disposer(delegate
			{
				this._resolveLeftMostUnqualifiedNameAsNamespaceOnly = false;
			});
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x0011F12C File Offset: 0x0011D32C
		internal MetadataMember ResolveMetadataMemberName(string[] name, ErrorContext errCtx)
		{
			MetadataMember metadataMember;
			if (name.Length == 1)
			{
				metadataMember = this.ResolveUnqualifiedName(name[0], false, errCtx);
			}
			else
			{
				metadataMember = this.ResolveFullyQualifiedName(name, name.Length, errCtx);
			}
			return metadataMember;
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x0011F15C File Offset: 0x0011D35C
		internal MetadataMember ResolveMetadataMemberAccess(MetadataMember qualifier, string name, ErrorContext errCtx)
		{
			string fullName = TypeResolver.GetFullName(new string[] { qualifier.Name, name });
			if (qualifier.MetadataMemberClass != MetadataMemberClass.Namespace)
			{
				if (qualifier.MetadataMemberClass == MetadataMemberClass.Type)
				{
					MetadataType metadataType = (MetadataType)qualifier;
					if (TypeSemantics.IsEnumerationType(metadataType.TypeUsage))
					{
						EnumMember enumMember;
						if (this._perspective.TryGetEnumMember((EnumType)metadataType.TypeUsage.EdmType, name, this._parserOptions.NameComparisonCaseInsensitive, out enumMember))
						{
							return new MetadataEnumMember(fullName, metadataType.TypeUsage, enumMember);
						}
						string text = Strings.NotAMemberOfType(name, qualifier.Name);
						throw EntitySqlException.Create(errCtx, text, null);
					}
				}
				string text2 = Strings.InvalidMetadataMemberClassResolution(qualifier.Name, qualifier.MetadataMemberClassName, MetadataNamespace.NamespaceClassName);
				throw EntitySqlException.Create(errCtx, text2, null);
			}
			MetadataType metadataType2;
			if (this.TryGetTypeFromMetadata(fullName, out metadataType2))
			{
				return metadataType2;
			}
			MetadataFunctionGroup metadataFunctionGroup;
			if (this.TryGetFunctionFromMetadata(qualifier.Name, name, out metadataFunctionGroup))
			{
				return metadataFunctionGroup;
			}
			return new MetadataNamespace(fullName);
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x0011F244 File Offset: 0x0011D444
		internal MetadataMember ResolveUnqualifiedName(string name, bool partOfQualifiedName, ErrorContext errCtx)
		{
			bool flag = partOfQualifiedName && this._resolveLeftMostUnqualifiedNameAsNamespaceOnly;
			bool flag2 = !partOfQualifiedName;
			InlineFunctionGroup inlineFunctionGroup;
			if (!flag && flag2 && this.TryGetInlineFunction(name, out inlineFunctionGroup))
			{
				return inlineFunctionGroup;
			}
			MetadataNamespace metadataNamespace;
			if (this._aliasedNamespaces.TryGetValue(name, out metadataNamespace))
			{
				return metadataNamespace;
			}
			if (!flag)
			{
				MetadataType metadataType = null;
				MetadataFunctionGroup metadataFunctionGroup = null;
				if (!this.TryGetTypeFromMetadata(name, out metadataType) && flag2)
				{
					string[] array = name.Split(new char[] { '.' });
					if (array.Length > 1)
					{
						if (array.All((string p) => p.Length > 0))
						{
							string text = array[array.Length - 1];
							string text2 = name.Substring(0, name.Length - text.Length - 1);
							this.TryGetFunctionFromMetadata(text2, text, out metadataFunctionGroup);
						}
					}
				}
				MetadataNamespace metadataNamespace2 = null;
				foreach (MetadataNamespace metadataNamespace3 in this._namespaces)
				{
					string fullName = TypeResolver.GetFullName(new string[] { metadataNamespace3.Name, name });
					MetadataType metadataType2;
					if (this.TryGetTypeFromMetadata(fullName, out metadataType2))
					{
						if (metadataType != null || metadataFunctionGroup != null)
						{
							throw TypeResolver.AmbiguousMetadataMemberName(errCtx, name, metadataNamespace3, metadataNamespace2);
						}
						metadataType = metadataType2;
						metadataNamespace2 = metadataNamespace3;
					}
					MetadataFunctionGroup metadataFunctionGroup2;
					if (flag2 && this.TryGetFunctionFromMetadata(metadataNamespace3.Name, name, out metadataFunctionGroup2))
					{
						if (metadataType != null || metadataFunctionGroup != null)
						{
							throw TypeResolver.AmbiguousMetadataMemberName(errCtx, name, metadataNamespace3, metadataNamespace2);
						}
						metadataFunctionGroup = metadataFunctionGroup2;
						metadataNamespace2 = metadataNamespace3;
					}
				}
				if (metadataType != null)
				{
					return metadataType;
				}
				if (metadataFunctionGroup != null)
				{
					return metadataFunctionGroup;
				}
			}
			return new MetadataNamespace(name);
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0011F3F0 File Offset: 0x0011D5F0
		private MetadataMember ResolveFullyQualifiedName(string[] name, int length, ErrorContext errCtx)
		{
			MetadataMember metadataMember;
			if (length == 2)
			{
				metadataMember = this.ResolveUnqualifiedName(name[0], true, errCtx);
			}
			else
			{
				metadataMember = this.ResolveFullyQualifiedName(name, length - 1, errCtx);
			}
			string text = name[length - 1];
			return this.ResolveMetadataMemberAccess(metadataMember, text, errCtx);
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x0011F42C File Offset: 0x0011D62C
		private static Exception AmbiguousMetadataMemberName(ErrorContext errCtx, string name, MetadataNamespace ns1, MetadataNamespace ns2)
		{
			string text = Strings.AmbiguousMetadataMemberName(name, ns1.Name, (ns2 != null) ? ns2.Name : null);
			throw EntitySqlException.Create(errCtx, text, null);
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x0011F45C File Offset: 0x0011D65C
		private bool TryGetTypeFromMetadata(string typeFullName, out MetadataType type)
		{
			TypeUsage typeUsage;
			if (this._perspective.TryGetTypeByName(typeFullName, this._parserOptions.NameComparisonCaseInsensitive, out typeUsage))
			{
				type = new MetadataType(typeFullName, typeUsage);
				return true;
			}
			type = null;
			return false;
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x0011F494 File Offset: 0x0011D694
		internal bool TryGetFunctionFromMetadata(string namespaceName, string functionName, out MetadataFunctionGroup functionGroup)
		{
			IList<EdmFunction> list;
			if (this._perspective.TryGetFunctionByName(namespaceName, functionName, this._parserOptions.NameComparisonCaseInsensitive, out list))
			{
				functionGroup = new MetadataFunctionGroup(TypeResolver.GetFullName(new string[] { namespaceName, functionName }), list);
				return true;
			}
			functionGroup = null;
			return false;
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x0011F4E0 File Offset: 0x0011D6E0
		private bool TryGetInlineFunction(string functionName, out InlineFunctionGroup inlineFunctionGroup)
		{
			List<InlineFunctionInfo> list;
			if (this._includeInlineFunctions && this._functionDefinitions.TryGetValue(functionName, out list))
			{
				inlineFunctionGroup = new InlineFunctionGroup(functionName, list);
				return true;
			}
			inlineFunctionGroup = null;
			return false;
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x0011F514 File Offset: 0x0011D714
		internal static string GetFullName(params string[] names)
		{
			return string.Join(".", names);
		}

		// Token: 0x04001C7F RID: 7295
		private readonly Perspective _perspective;

		// Token: 0x04001C80 RID: 7296
		private readonly ParserOptions _parserOptions;

		// Token: 0x04001C81 RID: 7297
		private readonly Dictionary<string, MetadataNamespace> _aliasedNamespaces;

		// Token: 0x04001C82 RID: 7298
		private readonly HashSet<MetadataNamespace> _namespaces;

		// Token: 0x04001C83 RID: 7299
		private readonly Dictionary<string, List<InlineFunctionInfo>> _functionDefinitions;

		// Token: 0x04001C84 RID: 7300
		private bool _includeInlineFunctions;

		// Token: 0x04001C85 RID: 7301
		private bool _resolveLeftMostUnqualifiedNameAsNamespaceOnly;

		// Token: 0x02000C8B RID: 3211
		private sealed class TypeUsageStructuralComparer : IEqualityComparer<TypeUsage>
		{
			// Token: 0x06006BBC RID: 27580 RVA: 0x0017024B File Offset: 0x0016E44B
			private TypeUsageStructuralComparer()
			{
			}

			// Token: 0x1700118D RID: 4493
			// (get) Token: 0x06006BBD RID: 27581 RVA: 0x00170253 File Offset: 0x0016E453
			public static TypeResolver.TypeUsageStructuralComparer Instance
			{
				get
				{
					return TypeResolver.TypeUsageStructuralComparer._instance;
				}
			}

			// Token: 0x06006BBE RID: 27582 RVA: 0x0017025A File Offset: 0x0016E45A
			public bool Equals(TypeUsage x, TypeUsage y)
			{
				return TypeSemantics.IsStructurallyEqual(x, y);
			}

			// Token: 0x06006BBF RID: 27583 RVA: 0x00170263 File Offset: 0x0016E463
			public int GetHashCode(TypeUsage obj)
			{
				return 0;
			}

			// Token: 0x04003195 RID: 12693
			private static readonly TypeResolver.TypeUsageStructuralComparer _instance = new TypeResolver.TypeUsageStructuralComparer();
		}
	}
}
