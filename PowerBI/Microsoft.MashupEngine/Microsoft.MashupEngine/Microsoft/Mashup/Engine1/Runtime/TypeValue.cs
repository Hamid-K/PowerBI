using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200167E RID: 5758
	public abstract class TypeValue : StructureValue, ITypeValue, IValue
	{
		// Token: 0x17002620 RID: 9760
		// (get) Token: 0x060091BB RID: 37307 RVA: 0x001E48AD File Offset: 0x001E2AAD
		public static ListTypeValue List
		{
			get
			{
				return ListTypeValue.Any;
			}
		}

		// Token: 0x17002621 RID: 9761
		// (get) Token: 0x060091BC RID: 37308 RVA: 0x001E48B4 File Offset: 0x001E2AB4
		public static RecordTypeValue Record
		{
			get
			{
				return RecordTypeValue.Any;
			}
		}

		// Token: 0x17002622 RID: 9762
		// (get) Token: 0x060091BD RID: 37309 RVA: 0x001E48BB File Offset: 0x001E2ABB
		public static FunctionTypeValue Function
		{
			get
			{
				return FunctionTypeValue.Any;
			}
		}

		// Token: 0x17002623 RID: 9763
		// (get) Token: 0x060091BE RID: 37310 RVA: 0x001E48C2 File Offset: 0x001E2AC2
		public static TableTypeValue Table
		{
			get
			{
				return TableTypeValue.Any;
			}
		}

		// Token: 0x060091BF RID: 37311 RVA: 0x001E48CC File Offset: 0x001E2ACC
		public override string ToSource()
		{
			string text = TypeValue.GetTypeKind(this);
			if (this.TypeKind == ValueKind.Any)
			{
				text = (this.IsNullable ? "any" : "anynonnull");
			}
			else if (this.IsNullable && this.TypeKind != ValueKind.Null)
			{
				text = "nullable " + text;
			}
			return text;
		}

		// Token: 0x060091C0 RID: 37312 RVA: 0x001E491D File Offset: 0x001E2B1D
		public override string ToString()
		{
			return "Type";
		}

		// Token: 0x060091C1 RID: 37313 RVA: 0x001E4924 File Offset: 0x001E2B24
		public sealed override object ToOleDb(Type type)
		{
			return ValueMarshaller.ToOleDbString("[Type]", this, type);
		}

		// Token: 0x060091C2 RID: 37314 RVA: 0x0000336E File Offset: 0x0000156E
		public override void TestConnection()
		{
		}

		// Token: 0x060091C3 RID: 37315 RVA: 0x001E4934 File Offset: 0x001E2B34
		public static string GetTypeKind(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.None:
				return "none";
			case ValueKind.Any:
				return "any";
			case ValueKind.Null:
				return "null";
			case ValueKind.Time:
				return "time";
			case ValueKind.Date:
				return "date";
			case ValueKind.DateTime:
				return "datetime";
			case ValueKind.DateTimeZone:
				return "datetimezone";
			case ValueKind.Duration:
				return "duration";
			case ValueKind.Number:
				return "number";
			case ValueKind.Logical:
				return "logical";
			case ValueKind.Text:
				return "text";
			case ValueKind.Binary:
				return "binary";
			case ValueKind.List:
				return "list";
			case ValueKind.Record:
				return "record";
			case ValueKind.Table:
				return "table";
			case ValueKind.Function:
				return "function";
			case ValueKind.Type:
				return "type";
			case ValueKind.Action:
				return "action";
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060091C4 RID: 37316 RVA: 0x001E4A0C File Offset: 0x001E2C0C
		public static string GetTypeName(TypeValue type)
		{
			string text;
			if (TypeValue.TypeNames.TryGetValue(type, out text))
			{
				return text;
			}
			switch (type.TypeKind)
			{
			case ValueKind.None:
				return "None.Type";
			case ValueKind.Any:
				return "Any.Type";
			case ValueKind.Null:
				return "Null.Type";
			case ValueKind.Time:
				return "Time.Type";
			case ValueKind.Date:
				return "Date.Type";
			case ValueKind.DateTime:
				return "DateTime.Type";
			case ValueKind.DateTimeZone:
				return "DateTimeZone.Type";
			case ValueKind.Duration:
				return "Duration.Type";
			case ValueKind.Number:
				return "Number.Type";
			case ValueKind.Logical:
				return "Logical.Type";
			case ValueKind.Text:
				return "Text.Type";
			case ValueKind.Binary:
				return "Binary.Type";
			case ValueKind.List:
				return "List.Type";
			case ValueKind.Record:
				return "Record.Type";
			case ValueKind.Table:
				return "Table.Type";
			case ValueKind.Function:
				return "Function.Type";
			case ValueKind.Type:
				return "Type.Type";
			case ValueKind.Action:
				return "Action.Type";
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060091C5 RID: 37317 RVA: 0x001E4AF8 File Offset: 0x001E2CF8
		public Type ToClrType()
		{
			switch (this.TypeKind)
			{
			case ValueKind.None:
			case ValueKind.Any:
			case ValueKind.Null:
			case ValueKind.List:
			case ValueKind.Record:
			case ValueKind.Table:
			case ValueKind.Function:
			case ValueKind.Type:
			case ValueKind.Action:
				return typeof(object);
			case ValueKind.Time:
				return typeof(Time);
			case ValueKind.Date:
				return typeof(Date);
			case ValueKind.DateTime:
				return typeof(DateTime);
			case ValueKind.DateTimeZone:
				return typeof(DateTimeOffset);
			case ValueKind.Duration:
				return typeof(TimeSpan);
			case ValueKind.Number:
				if (base.Equals(TypeValue.Int16))
				{
					return typeof(short);
				}
				if (base.Equals(TypeValue.Int32))
				{
					return typeof(int);
				}
				if (base.Equals(TypeValue.Int64))
				{
					return typeof(long);
				}
				if (base.Equals(TypeValue.Int8))
				{
					return typeof(sbyte);
				}
				if (base.Equals(TypeValue.Byte))
				{
					return typeof(byte);
				}
				if (base.Equals(TypeValue.Single))
				{
					return typeof(float);
				}
				if (base.Equals(TypeValue.Decimal))
				{
					return typeof(decimal);
				}
				if (base.Equals(TypeValue.Currency))
				{
					return typeof(Currency);
				}
				if (base.Equals(TypeValue.Percentage))
				{
					return typeof(decimal);
				}
				return typeof(double);
			case ValueKind.Logical:
				return typeof(bool);
			case ValueKind.Text:
				return typeof(string);
			case ValueKind.Binary:
				return typeof(byte[]);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002624 RID: 9764
		// (get) Token: 0x060091C6 RID: 37318 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x060091C7 RID: 37319 RVA: 0x001E4CAD File Offset: 0x001E2EAD
		public override Value NewMeta(RecordValue metaValue)
		{
			if (metaValue.IsEmpty)
			{
				return this;
			}
			return new TypeValue.MetaFacetsTypeValue(this, metaValue, this.Facets);
		}

		// Token: 0x17002625 RID: 9765
		// (get) Token: 0x060091C8 RID: 37320 RVA: 0x001E4CC6 File Offset: 0x001E2EC6
		public virtual TypeFacets Facets
		{
			get
			{
				return TypeFacets.None;
			}
		}

		// Token: 0x060091C9 RID: 37321 RVA: 0x001E4CCD File Offset: 0x001E2ECD
		public virtual TypeValue NewFacets(TypeFacets facets)
		{
			if (facets.IsEmpty)
			{
				return this;
			}
			return new TypeValue.MetaFacetsTypeValue(this, this.MetaValue, facets);
		}

		// Token: 0x17002626 RID: 9766
		// (get) Token: 0x060091CA RID: 37322 RVA: 0x0006808E File Offset: 0x0006628E
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Type;
			}
		}

		// Token: 0x17002627 RID: 9767
		// (get) Token: 0x060091CB RID: 37323 RVA: 0x001E4CE6 File Offset: 0x001E2EE6
		public override TypeValue Type
		{
			get
			{
				return TypeValue._Type;
			}
		}

		// Token: 0x17002628 RID: 9768
		// (get) Token: 0x060091CC RID: 37324 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsDefaultType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060091CD RID: 37325 RVA: 0x001E4CED File Offset: 0x001E2EED
		public sealed override Value NewType(TypeValue type)
		{
			if (!type.NonNullable.Equals(TypeValue._Type))
			{
				throw ValueException.CastTypeMismatch(this, type);
			}
			return this;
		}

		// Token: 0x17002629 RID: 9769
		// (get) Token: 0x060091CE RID: 37326
		public abstract ValueKind TypeKind { get; }

		// Token: 0x1700262A RID: 9770
		// (get) Token: 0x060091CF RID: 37327 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public virtual object TypeIdentity
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700262B RID: 9771
		// (get) Token: 0x060091D0 RID: 37328
		public abstract bool IsNullable { get; }

		// Token: 0x1700262C RID: 9772
		// (get) Token: 0x060091D1 RID: 37329
		public abstract TypeValue Nullable { get; }

		// Token: 0x1700262D RID: 9773
		// (get) Token: 0x060091D2 RID: 37330
		public abstract TypeValue NonNullable { get; }

		// Token: 0x1700262E RID: 9774
		// (get) Token: 0x060091D3 RID: 37331 RVA: 0x001E4D0C File Offset: 0x001E2F0C
		public IEnumerable<string> KeyColumnNames
		{
			get
			{
				if (this.TypeKind != ValueKind.Table)
				{
					return new string[0];
				}
				TableTypeValue asTableType = this.AsTableType;
				TableKey primaryKey = asTableType.GetPrimaryKey();
				if (primaryKey == null)
				{
					return new string[0];
				}
				string[] array = new string[primaryKey.Columns.Length];
				for (int i = 0; i < primaryKey.Columns.Length; i++)
				{
					array[i] = asTableType.ItemType.Fields.Keys[primaryKey.Columns[i]];
				}
				return array;
			}
		}

		// Token: 0x1700262F RID: 9775
		// (get) Token: 0x060091D4 RID: 37332 RVA: 0x001E4D84 File Offset: 0x001E2F84
		public Value DomainValues
		{
			get
			{
				return TypeServices.GetDomain(this);
			}
		}

		// Token: 0x17002630 RID: 9776
		// (get) Token: 0x060091D5 RID: 37333 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002631 RID: 9777
		// (get) Token: 0x060091D6 RID: 37334 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override TypeValue AsType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002632 RID: 9778
		// (get) Token: 0x060091D7 RID: 37335 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsListType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002633 RID: 9779
		// (get) Token: 0x060091D8 RID: 37336 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsRecordType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002634 RID: 9780
		// (get) Token: 0x060091D9 RID: 37337 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsFunctionType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002635 RID: 9781
		// (get) Token: 0x060091DA RID: 37338 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsTableType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002636 RID: 9782
		// (get) Token: 0x060091DB RID: 37339 RVA: 0x001D7C4C File Offset: 0x001D5E4C
		public virtual ListTypeValue AsListType
		{
			get
			{
				throw ValueException.CastTypeMismatch(this, TypeValue.List);
			}
		}

		// Token: 0x17002637 RID: 9783
		// (get) Token: 0x060091DC RID: 37340 RVA: 0x001E4D8C File Offset: 0x001E2F8C
		public virtual RecordTypeValue AsRecordType
		{
			get
			{
				throw ValueException.CastTypeMismatch(this, TypeValue.Record);
			}
		}

		// Token: 0x17002638 RID: 9784
		// (get) Token: 0x060091DD RID: 37341 RVA: 0x001E4D99 File Offset: 0x001E2F99
		public virtual FunctionTypeValue AsFunctionType
		{
			get
			{
				throw ValueException.CastTypeMismatch(this, TypeValue.Function);
			}
		}

		// Token: 0x17002639 RID: 9785
		// (get) Token: 0x060091DE RID: 37342 RVA: 0x001E4DA6 File Offset: 0x001E2FA6
		public virtual TableTypeValue AsTableType
		{
			get
			{
				throw ValueException.CastTypeMismatch(this, TypeValue.Table);
			}
		}

		// Token: 0x060091DF RID: 37343
		public abstract bool IsCompatibleWith(TypeValue other);

		// Token: 0x060091E0 RID: 37344 RVA: 0x001E4DB3 File Offset: 0x001E2FB3
		public sealed override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsType && value.AsType.TypeKind == this.TypeKind && value.AsType.TypeIdentity == this.TypeIdentity;
		}

		// Token: 0x060091E1 RID: 37345 RVA: 0x001E4DE5 File Offset: 0x001E2FE5
		public sealed override int GetHashCode(_ValueComparer comparer)
		{
			return RuntimeHelpers.GetHashCode(this.TypeIdentity);
		}

		// Token: 0x060091E2 RID: 37346 RVA: 0x001E4DF2 File Offset: 0x001E2FF2
		public static TypeValue NewNumberType()
		{
			return new TypeValue.PrimitiveTypeValue(ValueKind.Number);
		}

		// Token: 0x1700263A RID: 9786
		// (get) Token: 0x060091E3 RID: 37347 RVA: 0x001E4DFA File Offset: 0x001E2FFA
		ITypeValue ITypeValue.NonNullable
		{
			get
			{
				return this.NonNullable;
			}
		}

		// Token: 0x1700263B RID: 9787
		// (get) Token: 0x060091E4 RID: 37348 RVA: 0x001E4E02 File Offset: 0x001E3002
		ITypeValue ITypeValue.Nullable
		{
			get
			{
				return this.Nullable;
			}
		}

		// Token: 0x1700263C RID: 9788
		// (get) Token: 0x060091E5 RID: 37349 RVA: 0x001E4E0A File Offset: 0x001E300A
		IFunctionTypeValue ITypeValue.AsFunctionType
		{
			get
			{
				return this.AsFunctionType;
			}
		}

		// Token: 0x1700263D RID: 9789
		// (get) Token: 0x060091E6 RID: 37350 RVA: 0x001E4E12 File Offset: 0x001E3012
		IListTypeValue ITypeValue.AsListType
		{
			get
			{
				return this.AsListType;
			}
		}

		// Token: 0x1700263E RID: 9790
		// (get) Token: 0x060091E7 RID: 37351 RVA: 0x001E4E1A File Offset: 0x001E301A
		IRecordTypeValue ITypeValue.AsRecordType
		{
			get
			{
				return this.AsRecordType;
			}
		}

		// Token: 0x1700263F RID: 9791
		// (get) Token: 0x060091E8 RID: 37352 RVA: 0x001E4E22 File Offset: 0x001E3022
		ITableTypeValue ITypeValue.AsTableType
		{
			get
			{
				return this.AsTableType;
			}
		}

		// Token: 0x17002640 RID: 9792
		// (get) Token: 0x060091E9 RID: 37353 RVA: 0x001E4E2A File Offset: 0x001E302A
		IValue ITypeValue.DomainValues
		{
			get
			{
				return this.DomainValues;
			}
		}

		// Token: 0x060091EA RID: 37354 RVA: 0x001E4E32 File Offset: 0x001E3032
		bool ITypeValue.IsCompatibleWith(ITypeValue type)
		{
			return this.IsCompatibleWith((TypeValue)type);
		}

		// Token: 0x04004E47 RID: 20039
		private const string placeholder = "[Type]";

		// Token: 0x04004E48 RID: 20040
		public static readonly TextValue Placeholder = TextValue.New("[Type]");

		// Token: 0x04004E49 RID: 20041
		public static readonly TypeValue Any = new TypeValue.AnyTypeValue();

		// Token: 0x04004E4A RID: 20042
		public static readonly TypeValue None = new TypeValue.NoneTypeValue();

		// Token: 0x04004E4B RID: 20043
		public new static readonly TypeValue Null = new TypeValue.NullTypeValue();

		// Token: 0x04004E4C RID: 20044
		public static readonly TypeValue Logical = new TypeValue.PrimitiveTypeValue(ValueKind.Logical);

		// Token: 0x04004E4D RID: 20045
		public static readonly TypeValue Number = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E4E RID: 20046
		public static readonly TypeValue Date = new TypeValue.PrimitiveTypeValue(ValueKind.Date);

		// Token: 0x04004E4F RID: 20047
		public static readonly TypeValue DateTime = new TypeValue.PrimitiveTypeValue(ValueKind.DateTime);

		// Token: 0x04004E50 RID: 20048
		public static readonly TypeValue DateTimeZone = new TypeValue.PrimitiveTypeValue(ValueKind.DateTimeZone);

		// Token: 0x04004E51 RID: 20049
		public static readonly TypeValue Time = new TypeValue.PrimitiveTypeValue(ValueKind.Time);

		// Token: 0x04004E52 RID: 20050
		public static readonly TypeValue Duration = new TypeValue.PrimitiveTypeValue(ValueKind.Duration);

		// Token: 0x04004E53 RID: 20051
		public static readonly TypeValue Text = new TypeValue.PrimitiveTypeValue(ValueKind.Text);

		// Token: 0x04004E54 RID: 20052
		public static readonly TypeValue Binary = new TypeValue.PrimitiveTypeValue(ValueKind.Binary);

		// Token: 0x04004E55 RID: 20053
		public static readonly TypeValue _Type = new TypeValue.PrimitiveTypeValue(ValueKind.Type);

		// Token: 0x04004E56 RID: 20054
		public static readonly TypeValue Byte = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E57 RID: 20055
		public static readonly TypeValue Int8 = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E58 RID: 20056
		public static readonly TypeValue Int16 = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E59 RID: 20057
		public static readonly TypeValue Int32 = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E5A RID: 20058
		public static readonly TypeValue Int64 = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E5B RID: 20059
		public static readonly TypeValue Single = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E5C RID: 20060
		public static readonly TypeValue Double = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E5D RID: 20061
		public static readonly TypeValue Decimal = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E5E RID: 20062
		public static readonly TypeValue Currency = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E5F RID: 20063
		public static readonly TypeValue Percentage = new TypeValue.PrimitiveTypeValue(ValueKind.Number);

		// Token: 0x04004E60 RID: 20064
		public static readonly TypeValue Character = new TypeValue.PrimitiveTypeValue(ValueKind.Text);

		// Token: 0x04004E61 RID: 20065
		public static readonly TypeValue SerializedText = new TypeValue.PrimitiveTypeValue(ValueKind.Text);

		// Token: 0x04004E62 RID: 20066
		public static readonly TypeValue Guid = new TypeValue.PrimitiveTypeValue(ValueKind.Text);

		// Token: 0x04004E63 RID: 20067
		public static readonly TypeValue Uri = new TypeValue.PrimitiveTypeValue(ValueKind.Text);

		// Token: 0x04004E64 RID: 20068
		public static readonly TypeValue Password = new TypeValue.PrimitiveTypeValue(ValueKind.Text);

		// Token: 0x04004E65 RID: 20069
		public static readonly TypeValue Action = new TypeValue.PrimitiveTypeValue(ValueKind.Action);

		// Token: 0x04004E66 RID: 20070
		private static readonly Dictionary<TypeValue, string> TypeNames = new Dictionary<TypeValue, string>
		{
			{
				TypeValue.Byte,
				"Byte.Type"
			},
			{
				TypeValue.Int8,
				"Int8.Type"
			},
			{
				TypeValue.Int16,
				"Int16.Type"
			},
			{
				TypeValue.Int32,
				"Int32.Type"
			},
			{
				TypeValue.Int64,
				"Int64.Type"
			},
			{
				TypeValue.Decimal,
				"Decimal.Type"
			},
			{
				TypeValue.Currency,
				"Currency.Type"
			},
			{
				TypeValue.Percentage,
				"Percentage.Type"
			},
			{
				TypeValue.Single,
				"Single.Type"
			},
			{
				TypeValue.Double,
				"Double.Type"
			},
			{
				TypeValue.Character,
				"Character.Type"
			}
		};

		// Token: 0x0200167F RID: 5759
		private class AnyTypeValue : TypeValue
		{
			// Token: 0x17002641 RID: 9793
			// (get) Token: 0x060091ED RID: 37357 RVA: 0x0017811C File Offset: 0x0017631C
			public override ValueKind TypeKind
			{
				get
				{
					return ValueKind.Any;
				}
			}

			// Token: 0x17002642 RID: 9794
			// (get) Token: 0x060091EE RID: 37358 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsNullable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002643 RID: 9795
			// (get) Token: 0x060091EF RID: 37359 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue Nullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002644 RID: 9796
			// (get) Token: 0x060091F0 RID: 37360 RVA: 0x001E5055 File Offset: 0x001E3255
			public override TypeValue NonNullable
			{
				get
				{
					return TypeValue.AnyTypeValue.nonNullableAny;
				}
			}

			// Token: 0x060091F1 RID: 37361 RVA: 0x001E505C File Offset: 0x001E325C
			public override bool IsCompatibleWith(TypeValue other)
			{
				return other.TypeKind == ValueKind.Any && other.IsNullable;
			}

			// Token: 0x04004E67 RID: 20071
			private static readonly TypeValue nonNullableAny = new TypeValue.AnyTypeValue.NonNullableAnyTypeValue();

			// Token: 0x02001680 RID: 5760
			private class NonNullableAnyTypeValue : TypeValue
			{
				// Token: 0x17002645 RID: 9797
				// (get) Token: 0x060091F4 RID: 37364 RVA: 0x0017811C File Offset: 0x0017631C
				public override ValueKind TypeKind
				{
					get
					{
						return ValueKind.Any;
					}
				}

				// Token: 0x17002646 RID: 9798
				// (get) Token: 0x060091F5 RID: 37365 RVA: 0x00002105 File Offset: 0x00000305
				public override bool IsNullable
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17002647 RID: 9799
				// (get) Token: 0x060091F6 RID: 37366 RVA: 0x001BE5CD File Offset: 0x001BC7CD
				public override TypeValue Nullable
				{
					get
					{
						return TypeValue.Any;
					}
				}

				// Token: 0x17002648 RID: 9800
				// (get) Token: 0x060091F7 RID: 37367 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
				public override TypeValue NonNullable
				{
					get
					{
						return this;
					}
				}

				// Token: 0x060091F8 RID: 37368 RVA: 0x001E507B File Offset: 0x001E327B
				public override bool IsCompatibleWith(TypeValue other)
				{
					return other.TypeKind == ValueKind.Any;
				}
			}
		}

		// Token: 0x02001681 RID: 5761
		private class NoneTypeValue : TypeValue
		{
			// Token: 0x17002649 RID: 9801
			// (get) Token: 0x060091FA RID: 37370 RVA: 0x001E5086 File Offset: 0x001E3286
			public override ValueKind TypeKind
			{
				get
				{
					return ValueKind.None;
				}
			}

			// Token: 0x1700264A RID: 9802
			// (get) Token: 0x060091FB RID: 37371 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700264B RID: 9803
			// (get) Token: 0x060091FC RID: 37372 RVA: 0x001E508A File Offset: 0x001E328A
			public override TypeValue Nullable
			{
				get
				{
					return TypeValue.Null;
				}
			}

			// Token: 0x1700264C RID: 9804
			// (get) Token: 0x060091FD RID: 37373 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060091FE RID: 37374 RVA: 0x001E5091 File Offset: 0x001E3291
			public override bool IsCompatibleWith(TypeValue other)
			{
				return other.TypeKind == ValueKind.Any || other.TypeKind == ValueKind.None;
			}
		}

		// Token: 0x02001682 RID: 5762
		private class NullTypeValue : TypeValue
		{
			// Token: 0x1700264D RID: 9805
			// (get) Token: 0x06009200 RID: 37376 RVA: 0x00002105 File Offset: 0x00000305
			public override ValueKind TypeKind
			{
				get
				{
					return ValueKind.Null;
				}
			}

			// Token: 0x1700264E RID: 9806
			// (get) Token: 0x06009201 RID: 37377 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsNullable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700264F RID: 9807
			// (get) Token: 0x06009202 RID: 37378 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue Nullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002650 RID: 9808
			// (get) Token: 0x06009203 RID: 37379 RVA: 0x001E50A8 File Offset: 0x001E32A8
			public override TypeValue NonNullable
			{
				get
				{
					return TypeValue.None;
				}
			}

			// Token: 0x06009204 RID: 37380 RVA: 0x001E50AF File Offset: 0x001E32AF
			public override bool IsCompatibleWith(TypeValue other)
			{
				return other.IsNullable;
			}
		}

		// Token: 0x02001683 RID: 5763
		private class PrimitiveTypeValue : TypeValue
		{
			// Token: 0x06009206 RID: 37382 RVA: 0x001E50B7 File Offset: 0x001E32B7
			public PrimitiveTypeValue(ValueKind kind)
			{
				this.kind = kind;
			}

			// Token: 0x17002651 RID: 9809
			// (get) Token: 0x06009207 RID: 37383 RVA: 0x001E50C6 File Offset: 0x001E32C6
			public override ValueKind TypeKind
			{
				get
				{
					return this.kind;
				}
			}

			// Token: 0x17002652 RID: 9810
			// (get) Token: 0x06009208 RID: 37384 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002653 RID: 9811
			// (get) Token: 0x06009209 RID: 37385 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002654 RID: 9812
			// (get) Token: 0x0600920A RID: 37386 RVA: 0x001E50CE File Offset: 0x001E32CE
			public override TypeValue Nullable
			{
				get
				{
					if (this.nullableType == null)
					{
						this.nullableType = new TypeValue.NullablePrimitiveType(this);
					}
					return this.nullableType;
				}
			}

			// Token: 0x0600920B RID: 37387 RVA: 0x001E50EA File Offset: 0x001E32EA
			public override bool IsCompatibleWith(TypeValue other)
			{
				return other.TypeKind == ValueKind.Any || this.kind == other.TypeKind;
			}

			// Token: 0x04004E68 RID: 20072
			private readonly ValueKind kind;

			// Token: 0x04004E69 RID: 20073
			private TypeValue nullableType;
		}

		// Token: 0x02001684 RID: 5764
		private class NullablePrimitiveType : TypeValue
		{
			// Token: 0x0600920C RID: 37388 RVA: 0x001E5105 File Offset: 0x001E3305
			public NullablePrimitiveType(TypeValue type)
			{
				this.type = type;
			}

			// Token: 0x17002655 RID: 9813
			// (get) Token: 0x0600920D RID: 37389 RVA: 0x001E5114 File Offset: 0x001E3314
			public override ValueKind TypeKind
			{
				get
				{
					return this.type.TypeKind;
				}
			}

			// Token: 0x17002656 RID: 9814
			// (get) Token: 0x0600920E RID: 37390 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsNullable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002657 RID: 9815
			// (get) Token: 0x0600920F RID: 37391 RVA: 0x001E5121 File Offset: 0x001E3321
			public override TypeValue NonNullable
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002658 RID: 9816
			// (get) Token: 0x06009210 RID: 37392 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue Nullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06009211 RID: 37393 RVA: 0x001E5129 File Offset: 0x001E3329
			public override bool IsCompatibleWith(TypeValue other)
			{
				return (other.TypeKind == ValueKind.Any || this.TypeKind == other.TypeKind) && other.IsNullable;
			}

			// Token: 0x04004E6A RID: 20074
			private readonly TypeValue type;
		}

		// Token: 0x02001685 RID: 5765
		private class MetaFacetsTypeValue : TypeValue
		{
			// Token: 0x06009212 RID: 37394 RVA: 0x001E514A File Offset: 0x001E334A
			public MetaFacetsTypeValue(TypeValue type, RecordValue meta, TypeFacets facets)
			{
				this.type = type;
				this.meta = meta;
				this.facets = facets;
			}

			// Token: 0x06009213 RID: 37395 RVA: 0x001E5167 File Offset: 0x001E3367
			private static TypeValue New(TypeValue type, RecordValue meta, TypeFacets facets)
			{
				if (!meta.IsEmpty || !facets.IsEmpty)
				{
					return new TypeValue.MetaFacetsTypeValue(type, meta, facets);
				}
				return type;
			}

			// Token: 0x17002659 RID: 9817
			// (get) Token: 0x06009214 RID: 37396 RVA: 0x001E5183 File Offset: 0x001E3383
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x1700265A RID: 9818
			// (get) Token: 0x06009215 RID: 37397 RVA: 0x001E518B File Offset: 0x001E338B
			public override TypeFacets Facets
			{
				get
				{
					return this.facets;
				}
			}

			// Token: 0x1700265B RID: 9819
			// (get) Token: 0x06009216 RID: 37398 RVA: 0x001E5193 File Offset: 0x001E3393
			public override ValueKind TypeKind
			{
				get
				{
					return this.type.TypeKind;
				}
			}

			// Token: 0x1700265C RID: 9820
			// (get) Token: 0x06009217 RID: 37399 RVA: 0x001E51A0 File Offset: 0x001E33A0
			public override bool IsNullable
			{
				get
				{
					return this.type.IsNullable;
				}
			}

			// Token: 0x1700265D RID: 9821
			// (get) Token: 0x06009218 RID: 37400 RVA: 0x001E51AD File Offset: 0x001E33AD
			public override TypeValue NonNullable
			{
				get
				{
					return this.type.NonNullable.NewFacets(this.facets);
				}
			}

			// Token: 0x1700265E RID: 9822
			// (get) Token: 0x06009219 RID: 37401 RVA: 0x001E51C5 File Offset: 0x001E33C5
			public override TypeValue Nullable
			{
				get
				{
					return this.type.Nullable.NewFacets(this.facets);
				}
			}

			// Token: 0x1700265F RID: 9823
			// (get) Token: 0x0600921A RID: 37402 RVA: 0x001E51DD File Offset: 0x001E33DD
			public override object TypeIdentity
			{
				get
				{
					return this.type.TypeIdentity;
				}
			}

			// Token: 0x0600921B RID: 37403 RVA: 0x001E51EA File Offset: 0x001E33EA
			public override Value NewMeta(RecordValue metaValue)
			{
				return TypeValue.MetaFacetsTypeValue.New(this.type, metaValue, this.facets);
			}

			// Token: 0x0600921C RID: 37404 RVA: 0x001E51FE File Offset: 0x001E33FE
			public override TypeValue NewFacets(TypeFacets facets)
			{
				return TypeValue.MetaFacetsTypeValue.New(this.type, this.meta, facets);
			}

			// Token: 0x0600921D RID: 37405 RVA: 0x001E5212 File Offset: 0x001E3412
			public override bool IsCompatibleWith(TypeValue other)
			{
				return this.type.IsCompatibleWith(other);
			}

			// Token: 0x04004E6B RID: 20075
			private readonly TypeValue type;

			// Token: 0x04004E6C RID: 20076
			private readonly RecordValue meta;

			// Token: 0x04004E6D RID: 20077
			private readonly TypeFacets facets;
		}
	}
}
