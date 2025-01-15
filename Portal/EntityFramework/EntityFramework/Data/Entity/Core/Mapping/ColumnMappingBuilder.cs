using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000521 RID: 1313
	internal class ColumnMappingBuilder
	{
		// Token: 0x060040BA RID: 16570 RVA: 0x000DACB5 File Offset: 0x000D8EB5
		public ColumnMappingBuilder(EdmProperty columnProperty, IList<EdmProperty> propertyPath)
		{
			Check.NotNull<EdmProperty>(columnProperty, "columnProperty");
			Check.NotNull<IList<EdmProperty>>(propertyPath, "propertyPath");
			this._columnProperty = columnProperty;
			this._propertyPath = propertyPath;
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x000DACE3 File Offset: 0x000D8EE3
		public IList<EdmProperty> PropertyPath
		{
			get
			{
				return this._propertyPath;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x000DACEB File Offset: 0x000D8EEB
		// (set) Token: 0x060040BD RID: 16573 RVA: 0x000DACF3 File Offset: 0x000D8EF3
		public EdmProperty ColumnProperty
		{
			get
			{
				return this._columnProperty;
			}
			internal set
			{
				this._columnProperty = value;
				if (this._scalarPropertyMapping != null)
				{
					this._scalarPropertyMapping.Column = this._columnProperty;
				}
			}
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x000DAD15 File Offset: 0x000D8F15
		internal void SetTarget(ScalarPropertyMapping scalarPropertyMapping)
		{
			this._scalarPropertyMapping = scalarPropertyMapping;
		}

		// Token: 0x04001678 RID: 5752
		private EdmProperty _columnProperty;

		// Token: 0x04001679 RID: 5753
		private readonly IList<EdmProperty> _propertyPath;

		// Token: 0x0400167A RID: 5754
		private ScalarPropertyMapping _scalarPropertyMapping;
	}
}
