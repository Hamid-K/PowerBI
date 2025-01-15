using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.AnalysisServices;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000967 RID: 2407
	internal abstract class MdxCubeContext : CubeContext
	{
		// Token: 0x06004494 RID: 17556 RVA: 0x000E684E File Offset: 0x000E4A4E
		public MdxCubeContext(MdxCubeContextProvider contextProvider, bool useMeasureType, QueryCubeExpression expression)
			: base(expression)
		{
			this.contextProvider = contextProvider;
			this.useMeasureType = useMeasureType;
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x000E6865 File Offset: 0x000E4A65
		public MdxCubeContext(MdxCubeContextProvider contextProvider, bool useMeasureType, QueryCubeExpression expression, IList<ParameterArguments> arguments)
			: base(expression, arguments)
		{
			this.contextProvider = contextProvider;
			this.useMeasureType = useMeasureType;
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x000E687E File Offset: 0x000E4A7E
		public override TableValue DisplayFolders
		{
			get
			{
				return this.contextProvider.DisplayFolders;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x000E688B File Offset: 0x000E4A8B
		public override TableValue Dimensions
		{
			get
			{
				if (this.dimensionsTable == null)
				{
					this.dimensionsTable = MdxCubeMetadata.NewDimensionsTable(this.contextProvider, this.contextProvider.Cube, base.ParameterArguments);
				}
				return this.dimensionsTable;
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x000E68BD File Offset: 0x000E4ABD
		public override TableValue Measures
		{
			get
			{
				return this.contextProvider.Measures;
			}
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06004499 RID: 17561 RVA: 0x00066554 File Offset: 0x00064754
		public override TableValue MeasureGroups
		{
			get
			{
				return TableValue.Empty;
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x000E68CA File Offset: 0x000E4ACA
		public override CubeContextProvider ContextProvider
		{
			get
			{
				return this.contextProvider;
			}
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x000E68D2 File Offset: 0x000E4AD2
		public override IEngineHost EngineHost
		{
			get
			{
				return this.contextProvider.EngineHost;
			}
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x000E68DF File Offset: 0x000E4ADF
		public sealed override TableValue GetAvailableProperties()
		{
			if (!this.contextProvider.Cube.SupportsProperties)
			{
				return CubePropertiesTableValue.Empty;
			}
			return ListValue.New(this.GetPropertiesData(base.CubeExpression.DimensionAttributes)).ToTable(CubePropertiesTableValue.Type);
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x000E6919 File Offset: 0x000E4B19
		public sealed override TableValue GetAvailableMeasureProperties()
		{
			return ListValue.New(this.GetMeasurePropertiesData(base.CubeExpression.Measures)).ToTable(CubeMeasurePropertiesTableValue.Type);
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x000E693C File Offset: 0x000E4B3C
		public sealed override IEnumerator<IValueReference> Evaluate()
		{
			Keys keys = CubeContext.GetKeys(base.CubeExpression);
			if (keys.Length == 0)
			{
				return EmptyEnumerator<IValueReference>.Instance;
			}
			return this.CreateResultEnumerator(keys);
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x000E696A File Offset: 0x000E4B6A
		protected virtual MdxCubeContext.ResultEnumerator CreateResultEnumerator(Keys keys)
		{
			return new MdxCubeContext.ResultEnumerator(this, base.CubeExpression, keys);
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x000E697C File Offset: 0x000E4B7C
		private static IdentifierCubeExpression GetCube(CubeExpression expression)
		{
			QueryCubeExpression queryCubeExpression = expression as QueryCubeExpression;
			if (queryCubeExpression != null)
			{
				return MdxCubeContext.GetCube(queryCubeExpression.From);
			}
			return (IdentifierCubeExpression)expression;
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x000E69A8 File Offset: 0x000E4BA8
		protected MdxCubeContext.ColumnInfo[] GetColumnInfos(QueryCubeExpression expression)
		{
			List<MdxCubeContext.ColumnInfo> list = new List<MdxCubeContext.ColumnInfo>(expression.DimensionAttributes.Count + expression.Properties.Count + expression.Measures.Count + expression.MeasureProperties.Count);
			foreach (IdentifierCubeExpression identifierCubeExpression in expression.DimensionAttributes)
			{
				string identifier = identifierCubeExpression.Identifier;
				MdxLevel mdxLevel = (MdxLevel)this.contextProvider.Cube.GetObject(identifier);
				list.Add(new MdxCubeContext.ColumnInfo(mdxLevel.Kind, OleDbType.BSTR, identifier + ".[MEMBER_CAPTION]", identifier + ".[MEMBER_UNIQUE_NAME]"));
			}
			foreach (IdentifierCubeExpression identifierCubeExpression2 in expression.Properties)
			{
				string identifier2 = identifierCubeExpression2.Identifier;
				MdxProperty mdxProperty = (MdxProperty)this.contextProvider.Cube.GetObject(identifier2);
				list.Add(new MdxCubeContext.ColumnInfo(mdxProperty.Kind, mdxProperty.Type, identifier2, (mdxProperty.Key != null) ? mdxProperty.Key.MdxIdentifier : null));
			}
			foreach (IdentifierCubeExpression identifierCubeExpression3 in expression.Measures)
			{
				string identifier3 = identifierCubeExpression3.Identifier;
				MdxMeasure mdxMeasure = (MdxMeasure)this.contextProvider.Cube.GetObject(identifier3);
				list.Add(new MdxCubeContext.ColumnInfo(mdxMeasure.Kind, this.useMeasureType ? mdxMeasure.Type : OleDbType.Variant, identifier3, null));
			}
			foreach (IdentifierCubeExpression identifierCubeExpression4 in expression.MeasureProperties)
			{
				string identifier4 = identifierCubeExpression4.Identifier;
				MdxCellProperty mdxCellProperty = (MdxCellProperty)this.contextProvider.Cube.GetObject(identifier4);
				list.Add(new MdxCubeContext.ColumnInfo(mdxCellProperty.Kind, mdxCellProperty.Type, identifier4, null));
			}
			return list.ToArray();
		}

		// Token: 0x060044A2 RID: 17570
		protected abstract IDataReader GetDataReader(QueryCubeExpression expression);

		// Token: 0x060044A3 RID: 17571 RVA: 0x000E6BE0 File Offset: 0x000E4DE0
		protected virtual TextValue GetPropertyKind(MdxDimension dimension, MdxProperty property)
		{
			return property.PropertyKind.ToTextValue();
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x000E6BF0 File Offset: 0x000E4DF0
		protected TextValue GetPropertyKind(MdxPropertyKind propertyKind)
		{
			return TextValue.New(propertyKind.ToCubePropertyKind().ToString());
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x000E6C16 File Offset: 0x000E4E16
		protected virtual IEnumerable<IValueReference> GetPropertiesData(IList<IdentifierCubeExpression> dimensionAttributes)
		{
			foreach (IdentifierCubeExpression dimensionAttribute in dimensionAttributes)
			{
				MdxCubeObject @object = this.contextProvider.Cube.GetObject(dimensionAttribute.Identifier);
				MdxLevel level = @object as MdxLevel;
				if (level == null)
				{
					throw ValueException.NewDataFormatError<Message1>(Strings.ValueException_InvalidDimensionAttribute(dimensionAttribute.Identifier), TextValue.New(dimensionAttribute.Identifier), null);
				}
				foreach (MdxProperty mdxProperty in level.Properties)
				{
					MdxDimension dimension = level.Hierarchy.Dimension;
					yield return RecordValue.New(CubePropertiesTableValue.Type.ItemType, new Value[]
					{
						TextValue.New(dimensionAttribute.Identifier),
						TextValue.New(mdxProperty.Name),
						TextValue.New(mdxProperty.Caption),
						this.GetPropertyKind(dimension, mdxProperty),
						TextValue.New(dimension.MdxIdentifier)
					});
				}
				IEnumerator<MdxProperty> enumerator2 = null;
				level = null;
				dimensionAttribute = null;
			}
			IEnumerator<IdentifierCubeExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000E6C2D File Offset: 0x000E4E2D
		protected virtual IEnumerable<IValueReference> GetMeasurePropertiesData(IList<IdentifierCubeExpression> measures)
		{
			foreach (IdentifierCubeExpression identifierCubeExpression in measures)
			{
				MdxCubeObject @object = this.contextProvider.Cube.GetObject(identifierCubeExpression.Identifier);
				MdxMeasure mdxMeasure = @object as MdxMeasure;
				if (mdxMeasure == null)
				{
					throw ValueException.NewDataFormatError<Message1>(Strings.ValueException_InvalidMeasure(identifierCubeExpression.Identifier), TextValue.New(identifierCubeExpression.Identifier), null);
				}
				foreach (MdxCellProperty mdxCellProperty in mdxMeasure.CellProperties)
				{
					yield return RecordValue.New(CubeMeasurePropertiesTableValue.Type.ItemType, new Value[]
					{
						TextValue.New(mdxCellProperty.MdxIdentifier),
						TextValue.New(mdxCellProperty.Name),
						TextValue.New(mdxCellProperty.Caption),
						TextValue.New(mdxMeasure.MdxIdentifier)
					});
				}
				IEnumerator<MdxCellProperty> enumerator2 = null;
				mdxMeasure = null;
			}
			IEnumerator<IdentifierCubeExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04002483 RID: 9347
		protected readonly MdxCubeContextProvider contextProvider;

		// Token: 0x04002484 RID: 9348
		private readonly bool useMeasureType;

		// Token: 0x04002485 RID: 9349
		private TableValue dimensionsTable;

		// Token: 0x02000968 RID: 2408
		protected struct ColumnInfo
		{
			// Token: 0x060044A7 RID: 17575 RVA: 0x000E6C44 File Offset: 0x000E4E44
			public ColumnInfo(MdxCubeObjectKind cubeObjectKind, OleDbType type, string valueColumnName, string mdxIdentifierColumnName)
			{
				this.type = type;
				this.cubeObjectKind = cubeObjectKind;
				this.valueColumnName = valueColumnName;
				this.mdxIdentifierColumnName = mdxIdentifierColumnName;
			}

			// Token: 0x170015F3 RID: 5619
			// (get) Token: 0x060044A8 RID: 17576 RVA: 0x000E6C63 File Offset: 0x000E4E63
			public OleDbType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x170015F4 RID: 5620
			// (get) Token: 0x060044A9 RID: 17577 RVA: 0x000E6C6B File Offset: 0x000E4E6B
			public MdxCubeObjectKind CubeObjectKind
			{
				get
				{
					return this.cubeObjectKind;
				}
			}

			// Token: 0x170015F5 RID: 5621
			// (get) Token: 0x060044AA RID: 17578 RVA: 0x000E6C73 File Offset: 0x000E4E73
			public string ValueColumnName
			{
				get
				{
					return this.valueColumnName;
				}
			}

			// Token: 0x170015F6 RID: 5622
			// (get) Token: 0x060044AB RID: 17579 RVA: 0x000E6C7B File Offset: 0x000E4E7B
			public string MdxIdentifierColumnName
			{
				get
				{
					return this.mdxIdentifierColumnName;
				}
			}

			// Token: 0x04002486 RID: 9350
			private readonly OleDbType type;

			// Token: 0x04002487 RID: 9351
			private readonly MdxCubeObjectKind cubeObjectKind;

			// Token: 0x04002488 RID: 9352
			private readonly string valueColumnName;

			// Token: 0x04002489 RID: 9353
			private readonly string mdxIdentifierColumnName;
		}

		// Token: 0x02000969 RID: 2409
		protected class ResultEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x060044AC RID: 17580 RVA: 0x000E6C83 File Offset: 0x000E4E83
			public ResultEnumerator(MdxCubeContext context, QueryCubeExpression expression, Keys keys)
			{
				this.context = context;
				this.expression = expression;
				this.keys = keys;
				this.columnInfos = this.context.GetColumnInfos(expression);
			}

			// Token: 0x170015F7 RID: 5623
			// (get) Token: 0x060044AD RID: 17581 RVA: 0x000E6CB2 File Offset: 0x000E4EB2
			public IValueReference Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x060044AE RID: 17582 RVA: 0x000E6CBA File Offset: 0x000E4EBA
			public void Dispose()
			{
				if (this.reader != null)
				{
					this.reader.Dispose();
					this.reader = null;
				}
				this.current = null;
				this.isDisposed = true;
			}

			// Token: 0x170015F8 RID: 5624
			// (get) Token: 0x060044AF RID: 17583 RVA: 0x000E6CE4 File Offset: 0x000E4EE4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060044B0 RID: 17584 RVA: 0x000E6CEC File Offset: 0x000E4EEC
			public virtual bool MoveNext()
			{
				if (this.isDisposed)
				{
					return false;
				}
				if (this.reader == null)
				{
					this.reader = this.context.GetDataReader(this.expression);
					this.returnedColumns = this.GetColumns(this.reader, this.columnInfos);
				}
				if (!this.reader.Read())
				{
					this.current = null;
					return false;
				}
				IValueReference[] array = new IValueReference[this.keys.Length];
				for (int i = 0; i < array.Length; i++)
				{
					MdxCubeContext.ColumnInfo columnInfo = this.columnInfos[i];
					object fieldValue = this.GetFieldValue(columnInfo.ValueColumnName);
					IValueReference valueReference = AnalysisServicesOleDbMarshaller.Marshal(columnInfo.Type, fieldValue, this.context.contextProvider.Resource);
					try
					{
						string text = (string)this.GetFieldValue(columnInfo.MdxIdentifierColumnName);
						MdxCubeObjectKind cubeObjectKind = columnInfo.CubeObjectKind;
						if (cubeObjectKind != MdxCubeObjectKind.Level)
						{
							if (cubeObjectKind == MdxCubeObjectKind.Property)
							{
								if (!string.IsNullOrEmpty(text))
								{
									valueReference = valueReference.Value.NewMeta(RecordValue.New(MdxCubeContext.ResultEnumerator.PropertyMetadataKeys, new Value[] { TextValue.New(text) }));
								}
							}
						}
						else
						{
							valueReference = valueReference.Value.NewMeta(RecordValue.New(MdxCubeContext.ResultEnumerator.MemberMetadataKeys, new Value[] { TextValue.NewOrNull(text) }));
						}
					}
					catch (ValueException)
					{
					}
					array[i] = valueReference;
				}
				this.current = RecordValue.New(this.keys, array);
				return true;
			}

			// Token: 0x060044B1 RID: 17585 RVA: 0x000091AE File Offset: 0x000073AE
			public void Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060044B2 RID: 17586 RVA: 0x000E6E60 File Offset: 0x000E5060
			protected virtual Dictionary<string, int> GetColumns(IDataReader reader, MdxCubeContext.ColumnInfo[] columnInfos)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					dictionary.Add(reader.GetName(i), i);
				}
				return dictionary;
			}

			// Token: 0x060044B3 RID: 17587 RVA: 0x000E6E94 File Offset: 0x000E5094
			private object GetFieldValue(string fieldName)
			{
				int num;
				if (fieldName != null && this.returnedColumns.TryGetValue(fieldName, out num) && !this.reader.IsDBNull(num))
				{
					return this.reader[num];
				}
				return null;
			}

			// Token: 0x0400248A RID: 9354
			private static readonly Keys MemberMetadataKeys = Keys.New("Cube.AttributeMemberId");

			// Token: 0x0400248B RID: 9355
			private static readonly Keys PropertyMetadataKeys = Keys.New("Cube.PropertyKey");

			// Token: 0x0400248C RID: 9356
			private readonly MdxCubeContext context;

			// Token: 0x0400248D RID: 9357
			private readonly QueryCubeExpression expression;

			// Token: 0x0400248E RID: 9358
			private readonly Keys keys;

			// Token: 0x0400248F RID: 9359
			private readonly MdxCubeContext.ColumnInfo[] columnInfos;

			// Token: 0x04002490 RID: 9360
			private IDataReader reader;

			// Token: 0x04002491 RID: 9361
			private Dictionary<string, int> returnedColumns;

			// Token: 0x04002492 RID: 9362
			private IValueReference current;

			// Token: 0x04002493 RID: 9363
			private bool isDisposed;
		}
	}
}
