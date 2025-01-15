using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001664 RID: 5732
	internal class TableValueDataReader : ITableReader, IDisposable
	{
		// Token: 0x06009101 RID: 37121 RVA: 0x001E1EE0 File Offset: 0x001E00E0
		public TableValueDataReader(TableValue value, bool removeSpecialColumns = true)
		{
			RecordTypeValue itemType = value.Type.AsTableType.ItemType;
			this.enumerator = value.GetEnumerator();
			this.defaults = new Value[itemType.Fields.Keys.Length];
			if (removeSpecialColumns)
			{
				for (int i = 0; i < this.defaults.Length; i++)
				{
					this.defaults[i] = TableValueDataReader.GetDefault(itemType.Fields[i]["Type"].AsType);
				}
			}
		}

		// Token: 0x06009102 RID: 37122 RVA: 0x001E1F6C File Offset: 0x001E016C
		private static Value GetDefault(TypeValue type)
		{
			string text;
			if (PreviewServices.IsDelayed(type, out text))
			{
				return TextValue.New("[" + text + "]");
			}
			if (type.TypeKind == ValueKind.Table)
			{
				return TableValue.Placeholder;
			}
			if (type.TypeKind == ValueKind.Function)
			{
				return FunctionValue.Placeholder;
			}
			if (type.TypeKind == ValueKind.Type)
			{
				return TypeValue.Placeholder;
			}
			if (type.TypeKind == ValueKind.Action)
			{
				return ActionValue.Placeholder;
			}
			return null;
		}

		// Token: 0x06009103 RID: 37123 RVA: 0x001E1FDA File Offset: 0x001E01DA
		public void Dispose()
		{
			if (this.enumerator != null)
			{
				this.enumerator.Dispose();
				this.enumerator = null;
			}
		}

		// Token: 0x170025EC RID: 9708
		// (get) Token: 0x06009104 RID: 37124 RVA: 0x001E1FF6 File Offset: 0x001E01F6
		public int Columns
		{
			get
			{
				return this.defaults.Length;
			}
		}

		// Token: 0x06009105 RID: 37125 RVA: 0x001E2000 File Offset: 0x001E0200
		public bool MoveNext()
		{
			this.row = null;
			return this.enumerator.MoveNext();
		}

		// Token: 0x170025ED RID: 9709
		public Value this[int column]
		{
			get
			{
				Value value = this.defaults[column];
				if (value == null)
				{
					if (this.row == null)
					{
						this.row = this.enumerator.Current.Value.AsRecord;
					}
					value = this.row[column];
				}
				return value;
			}
		}

		// Token: 0x04004DD8 RID: 19928
		private IEnumerator<IValueReference> enumerator;

		// Token: 0x04004DD9 RID: 19929
		private Value[] defaults;

		// Token: 0x04004DDA RID: 19930
		private RecordValue row;
	}
}
