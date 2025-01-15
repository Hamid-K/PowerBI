using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001099 RID: 4249
	internal static class DbTypeServices
	{
		// Token: 0x06006F29 RID: 28457 RVA: 0x0017F5ED File Offset: 0x0017D7ED
		public static TypeValue ConvertToUncomparable(TypeValue type)
		{
			return BinaryOperator.AddMeta.Invoke(type, DbTypeServices.UncomparableColumn).AsType;
		}

		// Token: 0x06006F2A RID: 28458 RVA: 0x0017F604 File Offset: 0x0017D804
		public static TypeValue PropagateDocumentation(TypeValue originalType, TypeValue type)
		{
			RecordValue asRecord = Library.Record.SelectFields.Invoke(originalType.MetaValue, DbTypeServices.DocumentationFields, Library.MissingField.Ignore).AsRecord;
			return BinaryOperator.AddMeta.Invoke(type, asRecord).AsType;
		}

		// Token: 0x06006F2B RID: 28459 RVA: 0x0017F642 File Offset: 0x0017D842
		public static TypeValue CreateTypeValue(TypeValue type, bool isNullable, bool isSearchable)
		{
			if (isNullable)
			{
				type = DbTypeServices.PropagateDocumentation(type, type.Nullable).AsType;
			}
			if (!isSearchable)
			{
				type = DbTypeServices.ConvertToUncomparable(type);
			}
			return type;
		}

		// Token: 0x06006F2C RID: 28460 RVA: 0x0017F668 File Offset: 0x0017D868
		public static bool IsComparable(TypeValue type)
		{
			Value value;
			return TypeServices.IsScalar(type) && (!type.TryGetMetaField("Uncomparable", out value) || !value.IsLogical || !value.AsBoolean);
		}

		// Token: 0x06006F2D RID: 28461 RVA: 0x0017F6A1 File Offset: 0x0017D8A1
		public static bool IsCompatibleType(TypeValue left, TypeValue right, BinaryOperator2 @operator)
		{
			left = TypeServices.StripNullableAndMetadata(left);
			right = TypeServices.StripNullableAndMetadata(right);
			return TypeServices.IsCompatibleType(left, right);
		}

		// Token: 0x04003DA6 RID: 15782
		public const string ColumnIsUncomparable = "Uncomparable";

		// Token: 0x04003DA7 RID: 15783
		private static readonly RecordValue UncomparableColumn = RecordValue.New(Keys.New("Uncomparable"), new Value[] { LogicalValue.True });

		// Token: 0x04003DA8 RID: 15784
		private static readonly ListValue DocumentationFields = ListValue.New(new Value[]
		{
			TextValue.New("Documentation.Description"),
			TextValue.New("Documentation.FieldDescription"),
			TextValue.New("Documentation.FieldCaption"),
			TextValue.New("Documentation.CreatedDate"),
			TextValue.New("Documentation.ModifiedDate"),
			TextValue.New("Documentation.NativeSize"),
			TextValue.New("Documentation.IsWritable")
		});
	}
}
