using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000531 RID: 1329
	public sealed class FunctionImportComplexTypeMapping : FunctionImportStructuralTypeMapping
	{
		// Token: 0x0600418B RID: 16779 RVA: 0x000DD592 File Offset: 0x000DB792
		public FunctionImportComplexTypeMapping(ComplexType returnType, Collection<FunctionImportReturnTypePropertyMapping> properties)
			: this(Check.NotNull<ComplexType>(returnType, "returnType"), Check.NotNull<Collection<FunctionImportReturnTypePropertyMapping>>(properties, "properties"), LineInfo.Empty)
		{
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x000DD5B5 File Offset: 0x000DB7B5
		internal FunctionImportComplexTypeMapping(ComplexType returnType, Collection<FunctionImportReturnTypePropertyMapping> properties, LineInfo lineInfo)
			: base(properties, lineInfo)
		{
			this._returnType = returnType;
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x000DD5C6 File Offset: 0x000DB7C6
		public ComplexType ReturnType
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x040016BB RID: 5819
		private readonly ComplexType _returnType;
	}
}
