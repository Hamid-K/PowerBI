using System;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Xml.XPath;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000535 RID: 1333
	public sealed class FunctionImportEntityTypeMappingConditionValue : FunctionImportEntityTypeMappingCondition
	{
		// Token: 0x060041A0 RID: 16800 RVA: 0x000DD764 File Offset: 0x000DB964
		public FunctionImportEntityTypeMappingConditionValue(string columnName, object value)
			: base(Check.NotNull<string>(columnName, "columnName"), LineInfo.Empty)
		{
			Check.NotNull<object>(value, "value");
			this._value = value;
			this._convertedValues = new Memoizer<Type, object>(new Func<Type, object>(this.GetConditionValue), null);
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x000DD7B2 File Offset: 0x000DB9B2
		internal FunctionImportEntityTypeMappingConditionValue(string columnName, XPathNavigator columnValue, LineInfo lineInfo)
			: base(columnName, lineInfo)
		{
			this._xPathValue = columnValue;
			this._convertedValues = new Memoizer<Type, object>(new Func<Type, object>(this.GetConditionValue), null);
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x000DD7DB File Offset: 0x000DB9DB
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x000DD7E3 File Offset: 0x000DB9E3
		internal override ValueCondition ConditionValue
		{
			get
			{
				return new ValueCondition((this._value != null) ? this._value.ToString() : this._xPathValue.Value);
			}
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x000DD80C File Offset: 0x000DBA0C
		internal override bool ColumnValueMatchesCondition(object columnValue)
		{
			if (columnValue == null || Convert.IsDBNull(columnValue))
			{
				return false;
			}
			Type type = columnValue.GetType();
			object obj = this._convertedValues.Evaluate(type);
			return ByValueEqualityComparer.Default.Equals(columnValue, obj);
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x000DD848 File Offset: 0x000DBA48
		private object GetConditionValue(Type columnValueType)
		{
			return this.GetConditionValue(columnValueType, delegate
			{
				throw new EntityCommandExecutionException(Strings.Mapping_FunctionImport_UnsupportedType(this.ColumnName, columnValueType.FullName));
			}, delegate
			{
				throw new EntityCommandExecutionException(Strings.Mapping_FunctionImport_ConditionValueTypeMismatch("FunctionImportMapping", this.ColumnName, columnValueType.FullName));
			});
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x000DD890 File Offset: 0x000DBA90
		internal object GetConditionValue(Type columnValueType, Action handleTypeNotComparable, Action handleInvalidConditionValue)
		{
			PrimitiveType primitiveType;
			if (!ClrProviderManifest.Instance.TryGetPrimitiveType(columnValueType, out primitiveType) || !MappingItemLoader.IsTypeSupportedForCondition(primitiveType.PrimitiveTypeKind))
			{
				handleTypeNotComparable();
				return null;
			}
			if (this._value == null)
			{
				object obj;
				try
				{
					obj = this._xPathValue.ValueAs(columnValueType);
				}
				catch (FormatException)
				{
					handleInvalidConditionValue();
					obj = null;
				}
				return obj;
			}
			if (this._value.GetType() == columnValueType)
			{
				return this._value;
			}
			handleInvalidConditionValue();
			return null;
		}

		// Token: 0x040016C2 RID: 5826
		private readonly object _value;

		// Token: 0x040016C3 RID: 5827
		private readonly XPathNavigator _xPathValue;

		// Token: 0x040016C4 RID: 5828
		private readonly Memoizer<Type, object> _convertedValues;
	}
}
