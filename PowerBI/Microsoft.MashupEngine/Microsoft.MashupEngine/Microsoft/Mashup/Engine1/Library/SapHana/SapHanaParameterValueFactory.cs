using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000473 RID: 1139
	internal sealed class SapHanaParameterValueFactory
	{
		// Token: 0x060025E9 RID: 9705 RVA: 0x0006DAB0 File Offset: 0x0006BCB0
		public SapHanaParameterValueFactory(SapHanaOdbcDataSource dataSource, CubeValue cube)
		{
			this.dataSource = dataSource;
			this.cube = cube;
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x0006DAC8 File Offset: 0x0006BCC8
		public FunctionValue NewParameterValue(SapHanaParameter parameter)
		{
			switch (parameter.SelectionType)
			{
			case SapHanaSelectionType.SingleValue:
				return this.NewSingleValue(parameter);
			case SapHanaSelectionType.Interval:
				return this.NewInterval(parameter);
			case SapHanaSelectionType.Range:
				return this.NewRange(parameter);
			default:
				throw new InvalidOperationException(parameter.SelectionType.ToString());
			}
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x0006DB24 File Offset: 0x0006BD24
		private FunctionValue NewInterval(SapHanaParameter parameter)
		{
			TypeValue typeValue = this.GetTypeValue(parameter);
			TypeValue[] array = new TypeValue[] { typeValue, typeValue };
			IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(parameter.Name);
			return new ParameterValue(this.cube, identifierCubeExpression, 2, SapHanaParameterValueFactory.intervalParameterNames, array);
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x0006DB68 File Offset: 0x0006BD68
		private FunctionValue NewRange(SapHanaParameter parameter)
		{
			TypeValue typeValue = this.GetTypeValue(parameter);
			TypeValue[] array = new TypeValue[]
			{
				SapHanaModule.RangeOperator.Type,
				typeValue,
				typeValue.Nullable.NewMeta(typeValue.MetaValue).AsType
			};
			IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(parameter.Name);
			return new ParameterValue(this.cube, identifierCubeExpression, 2, SapHanaParameterValueFactory.rangeParameterNames, array);
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0006DBC8 File Offset: 0x0006BDC8
		private FunctionValue NewSingleValue(SapHanaParameter parameter)
		{
			TypeValue typeValue = this.GetTypeValue(parameter);
			TypeValue[] array = new TypeValue[] { parameter.IsMultiline ? ListTypeValue.New(typeValue) : typeValue };
			IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(parameter.Name);
			return new ParameterValue(this.cube, identifierCubeExpression, 1, SapHanaParameterValueFactory.singleValueParameterNames, array);
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0006DC18 File Offset: 0x0006BE18
		private TypeValue GetTypeValue(SapHanaParameter parameter)
		{
			TypeValue typeValue = OdbcTypeMap.FromSqlType(this.dataSource.Types.GetType(parameter.DataTypeName).SqlType).TypeValue;
			RecordValue recordValue = typeValue.MetaValue;
			if (parameter.HasValues)
			{
				RecordTypeValue itemType = RecordTypeValue.New(RecordValue.New(SapHanaParameterValueFactory.valueQueryKeys, new Value[]
				{
					RecordTypeAlgebra.NewField(typeValue, false),
					RecordTypeAlgebra.NewField(TypeValue.Text, false)
				}));
				ListValue listValue = ListValue.New(DeferredEnumerable.From<IValueReference>(delegate
				{
					IDataReader values = parameter.GetValues();
					IEnumerator<IValueReference> enumerator;
					try
					{
						enumerator = DbDataReaderEnumerator.New(this.dataSource.Host, values, true, "SAPHANA", itemType, this.dataSource.Resource);
					}
					catch
					{
						values.Dispose();
						throw;
					}
					return enumerator;
				}).Select(new Func<IValueReference, IValueReference>(SapHanaParameterValueFactory.GetValue)));
				recordValue = RecordValue.New(SapHanaParameterValueFactory.allowedValuesMetaType, new Value[] { listValue });
				recordValue = recordValue.Concatenate(NavigationTableServices.NewAllowedValuesIsOpenSetMetadata(true)).AsRecord;
			}
			else if (parameter.IsMultiline)
			{
				recordValue = NavigationTableServices.NewAllowedValuesIsOpenSetMetadata(true);
			}
			if (parameter.DefaultValues != null)
			{
				if (parameter.IsMultiline)
				{
					recordValue = recordValue.Concatenate(NavigationTableServices.NewDefaultValuesMetadata(parameter.DefaultValues)).AsRecord;
				}
				else if (parameter.DefaultValues.Any<Value>())
				{
					recordValue = recordValue.Concatenate(NavigationTableServices.NewDefaultValueMetadata(parameter.DefaultValues[0])).AsRecord;
				}
			}
			if (typeValue.MetaValue != recordValue)
			{
				return typeValue.NewMeta(recordValue).AsType;
			}
			return typeValue;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0006DD98 File Offset: 0x0006BF98
		private static IValueReference GetValue(IValueReference valueReference)
		{
			RecordValue asRecord = valueReference.Value.AsRecord;
			Value value = asRecord[0];
			Value value2 = asRecord[1];
			return value.NewMeta(RecordValue.New(Keys.New("Documentation.Caption"), new Value[] { value2 }));
		}

		// Token: 0x04000FD6 RID: 4054
		private static readonly string[] intervalParameterNames = new string[] { "start", "end" };

		// Token: 0x04000FD7 RID: 4055
		private static readonly string[] singleValueParameterNames = new string[] { "value" };

		// Token: 0x04000FD8 RID: 4056
		private static readonly string[] rangeParameterNames = new string[] { "operator", "start", "end" };

		// Token: 0x04000FD9 RID: 4057
		private static readonly TypeValue DelayedNullableTableType = PreviewServices.ConvertToDelayedValue(TypeValue.Table.Nullable, "Value");

		// Token: 0x04000FDA RID: 4058
		private static readonly RecordTypeValue allowedValuesMetaType = RecordTypeValue.New(RecordValue.New(new NamedValue[]
		{
			new NamedValue("Documentation.AllowedValues", RecordTypeAlgebra.NewField(SapHanaParameterValueFactory.DelayedNullableTableType, false))
		}), false);

		// Token: 0x04000FDB RID: 4059
		private static readonly Keys valueQueryKeys = Keys.New("VALUE", "DESCRIPTION");

		// Token: 0x04000FDC RID: 4060
		private readonly SapHanaOdbcDataSource dataSource;

		// Token: 0x04000FDD RID: 4061
		private readonly CubeValue cube;
	}
}
