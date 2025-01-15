using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200039E RID: 926
	internal class OpenApiTableValue : TableValue
	{
		// Token: 0x06002039 RID: 8249 RVA: 0x00054ADE File Offset: 0x00052CDE
		public OpenApiTableValue(OpenApiDocument document)
		{
			this.document = document;
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x00054AED File Offset: 0x00052CED
		public override TypeValue Type
		{
			get
			{
				return OpenApiTableValue.type;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x00054AF4 File Offset: 0x00052CF4
		public override RecordValue MetaValue
		{
			get
			{
				return this.document.GetMetadata();
			}
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00054B01 File Offset: 0x00052D01
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			IEnumerable<RecordValue> rawNavTable = this.GetRawNavTable();
			if (this.duplicateNames == null)
			{
				this.duplicateNames = new HashSet<string>();
				HashSet<string> hashSet = new HashSet<string>();
				foreach (RecordValue recordValue in rawNavTable)
				{
					string asString = recordValue["Name"].AsText.AsString;
					if (!hashSet.Add(asString))
					{
						this.duplicateNames.Add(asString);
					}
				}
			}
			foreach (RecordValue recordValue2 in rawNavTable)
			{
				if (!this.duplicateNames.Contains(recordValue2["Name"].AsText.AsString))
				{
					yield return recordValue2;
				}
				else
				{
					yield return this.CreateDuplicatedNameRow(recordValue2);
				}
			}
			IEnumerator<RecordValue> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00054B10 File Offset: 0x00052D10
		private IEnumerable<RecordValue> GetRawNavTable()
		{
			foreach (KeyValuePair<string, OpenApiPathItem> keyValuePair in this.document.Paths)
			{
				string key = keyValuePair.Key;
				if (keyValuePair.Value != null && keyValuePair.Value.Get != null && (!keyValuePair.Value.Get.Deprecated.Value || this.document.UserSettings.IncludeDeprecated))
				{
					yield return this.CreateRow(keyValuePair.Value.Get, key);
				}
			}
			IEnumerator<KeyValuePair<string, OpenApiPathItem>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00054B20 File Offset: 0x00052D20
		private RecordValue CreateRow(OpenApiOperationDefinition operation, string pathName)
		{
			FunctionValue functionValue = OpenApiFunctionValue.New(this.document, operation, pathName);
			return RecordValue.New(this.Columns, new Value[]
			{
				TextValue.New(this.GetDisplayName(operation, pathName)),
				functionValue,
				TextValue.New("Function")
			});
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x00054B70 File Offset: 0x00052D70
		private RecordValue CreateDuplicatedNameRow(RecordValue row)
		{
			Value value = row["Name"];
			ExceptionValueReference exceptionValueReference = new ExceptionValueReference(ValueException.KeyNotUnique(value));
			return RecordValue.New(this.Columns, new IValueReference[] { value, exceptionValueReference, exceptionValueReference });
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x00054BB4 File Offset: 0x00052DB4
		private string GetDisplayName(OpenApiOperationDefinition operation, string operationName)
		{
			string text;
			if (!string.IsNullOrEmpty(operation.OperationId))
			{
				text = operation.OperationId;
			}
			else
			{
				string[] array = operationName.Split(new char[] { '/', '{', '}' });
				text = string.Join("_", array);
				text += "_get";
			}
			return text;
		}

		// Token: 0x04000C49 RID: 3145
		private static readonly Keys NavigationTableWithIdKindKeys = Keys.New("Name", "Data", "Kind");

		// Token: 0x04000C4A RID: 3146
		private static readonly TableTypeValue type = NavigationTableServices.AddDataColumnIsLeafMetadata(NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(OpenApiTableValue.NavigationTableWithIdKindKeys, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Function, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		})), new TableKey[]
		{
			new TableKey(new int[1], true)
		}))).AsTableType;

		// Token: 0x04000C4B RID: 3147
		private readonly OpenApiDocument document;

		// Token: 0x04000C4C RID: 3148
		private HashSet<string> duplicateNames;
	}
}
