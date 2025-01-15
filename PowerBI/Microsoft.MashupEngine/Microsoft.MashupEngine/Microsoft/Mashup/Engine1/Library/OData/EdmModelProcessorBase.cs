using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000720 RID: 1824
	internal abstract class EdmModelProcessorBase<TOutput> where TOutput : EdmModelProcessorOutputBase, new()
	{
		// Token: 0x0600364F RID: 13903 RVA: 0x000AD236 File Offset: 0x000AB436
		protected EdmModelProcessorBase(IEngineHost engineHost)
		{
			this.engineHost = engineHost;
			this.tableKeys = new Dictionary<TypeValue, TableKey>();
			this.tableAnnotations = new Dictionary<TypeValue, List<NamedValue>>();
			this.output = new TOutput();
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000AD268 File Offset: 0x000AB468
		internal TOutput Build(HttpResource resource)
		{
			this.output = new TOutput();
			try
			{
				this.ProcessTypes();
				this.ProcessEntityContainers();
			}
			catch (InvalidOperationException ex)
			{
				throw ODataCommonErrors.InvalidMetadataDocument(this.engineHost, ex, resource);
			}
			return this.output;
		}

		// Token: 0x06003651 RID: 13905
		protected abstract void ProcessTypes();

		// Token: 0x06003652 RID: 13906
		protected abstract void ProcessEntityContainers();

		// Token: 0x06003653 RID: 13907 RVA: 0x000AD2B4 File Offset: 0x000AB4B4
		protected TypeValue SetQueryableAttributeFalse(TypeValue typeValue)
		{
			return BinaryOperator.AddMeta.Invoke(typeValue, RecordValue.New(EdmModelProcessorBase<TOutput>.QueryableReturnableKeys, new Value[]
			{
				LogicalValue.New(false),
				LogicalValue.New(true)
			})).AsType;
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000AD2E8 File Offset: 0x000AB4E8
		protected TypeValue SetEdmTypeAttribute(TypeValue typeValue, string edmTypeName)
		{
			return BinaryOperator.AddMeta.Invoke(typeValue, RecordValue.New(EdmModelProcessorBase<TOutput>.EdmTypeKeys, new Value[] { TextValue.New(edmTypeName) })).AsType;
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000AD314 File Offset: 0x000AB514
		protected ListTypeValue CreateListType(TypeValue elementType)
		{
			ListTypeValue listTypeValue = ListTypeValue.New(elementType);
			List<NamedValue> list = null;
			if (this.tableAnnotations.TryGetValue(elementType, out list))
			{
				RecordValue recordValue = RecordValue.New(list.ToArray());
				listTypeValue = listTypeValue.NewMeta(recordValue).AsType.AsListType;
			}
			return listTypeValue;
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000AD35C File Offset: 0x000AB55C
		protected TableTypeValue CreateTableType(TypeValue elementType)
		{
			IList<TableKey> list = this.GetTableKeys(elementType);
			TableTypeValue tableTypeValue = TableTypeValue.New(elementType.AsRecordType, list);
			List<NamedValue> list2 = null;
			if (this.tableAnnotations.TryGetValue(elementType, out list2))
			{
				RecordValue recordValue = RecordValue.New(list2.ToArray());
				tableTypeValue = tableTypeValue.NewMeta(recordValue).AsType.AsTableType;
			}
			return tableTypeValue;
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000AD3B0 File Offset: 0x000AB5B0
		protected IList<TableKey> GetTableKeys(TypeValue type)
		{
			TableKey tableKey;
			this.tableKeys.TryGetValue(type, out tableKey);
			if (tableKey != null)
			{
				return new List<TableKey> { tableKey };
			}
			return new List<TableKey>();
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000AD3E4 File Offset: 0x000AB5E4
		public static string GetOtherColumnsColumnName(IEnumerable<string> keys)
		{
			HashSet<string> hashSet = new HashSet<string>(keys);
			int num = 0;
			string text = "More Columns";
			while (hashSet.Contains(text))
			{
				num++;
				text = "More Columns" + num.ToString(CultureInfo.InvariantCulture);
			}
			return text;
		}

		// Token: 0x04001BE6 RID: 7142
		public const string Queryable = "Queryable";

		// Token: 0x04001BE7 RID: 7143
		public const string Returned = "Returned";

		// Token: 0x04001BE8 RID: 7144
		public const string EdmType = "EdmType";

		// Token: 0x04001BE9 RID: 7145
		public const string ParameterEdmTypeMetadata = "EdmParameterType";

		// Token: 0x04001BEA RID: 7146
		private static readonly Keys QueryableReturnableKeys = Keys.New("Queryable", "Returned");

		// Token: 0x04001BEB RID: 7147
		private static readonly Keys EdmTypeKeys = Keys.New("EdmType");

		// Token: 0x04001BEC RID: 7148
		protected static readonly RecordValue OtherFieldsColumnTypeField = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			RecordTypeValue.Any,
			LogicalValue.False
		});

		// Token: 0x04001BED RID: 7149
		protected readonly IEngineHost engineHost;

		// Token: 0x04001BEE RID: 7150
		protected readonly Dictionary<TypeValue, TableKey> tableKeys;

		// Token: 0x04001BEF RID: 7151
		protected readonly Dictionary<TypeValue, List<NamedValue>> tableAnnotations;

		// Token: 0x04001BF0 RID: 7152
		protected TOutput output;
	}
}
