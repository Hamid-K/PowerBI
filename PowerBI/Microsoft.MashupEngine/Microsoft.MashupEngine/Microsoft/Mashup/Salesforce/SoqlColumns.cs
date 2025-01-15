using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001F5 RID: 501
	internal class SoqlColumns
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x0001621C File Offset: 0x0001441C
		private SoqlColumns(Keys names, IList<SoqlExpression> expressions, IValueReference[] types, bool[] isAggregate)
		{
			this.names = names;
			this.expressions = expressions;
			this.types = types;
			this.isAggregate = isAggregate;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00016244 File Offset: 0x00014444
		public SoqlColumns(Keys names, IList<SoqlExpression> expressions, IValueReference[] types, bool isAggregate = false)
		{
			this.names = names;
			this.expressions = expressions;
			this.types = types;
			this.isAggregate = new bool[names.Length];
			if (isAggregate)
			{
				for (int i = 0; i < names.Length; i++)
				{
					this.isAggregate[i] = true;
				}
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001629C File Offset: 0x0001449C
		public SoqlColumns(RecordTypeValue recordType)
		{
			this.names = recordType.Fields.Keys;
			this.expressions = new SoqlExpression[this.names.Length];
			this.types = new IValueReference[this.names.Length];
			this.isAggregate = new bool[this.names.Length];
			for (int i = 0; i < this.names.Length; i++)
			{
				TypeValue asType = recordType.Fields[i]["Type"].AsType;
				this.types[i] = asType;
				this.expressions[i] = new SoqlExpression(recordType, null, asType, new ColumnAccessQueryExpression(i));
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00016357 File Offset: 0x00014557
		public int Length
		{
			get
			{
				return this.names.Length;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00016364 File Offset: 0x00014564
		public Keys Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0001636C File Offset: 0x0001456C
		public IList<SoqlExpression> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00016374 File Offset: 0x00014574
		public IValueReference[] Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0001637C File Offset: 0x0001457C
		public bool[] IsAggregate
		{
			get
			{
				return this.isAggregate;
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00016384 File Offset: 0x00014584
		public SoqlColumns SelectColumns(ColumnSelection columnSelection)
		{
			SoqlExpression[] array = new SoqlExpression[columnSelection.Keys.Length];
			IValueReference[] array2 = new IValueReference[columnSelection.Keys.Length];
			bool[] array3 = new bool[columnSelection.Keys.Length];
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				int column = columnSelection.GetColumn(i);
				array[i] = this.expressions[column];
				array2[i] = this.types[column];
				array3[i] = this.isAggregate[column];
			}
			return new SoqlColumns(columnSelection.Keys, array, array2, array3);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001641C File Offset: 0x0001461C
		public SoqlColumns SelectColumns(Keys keys)
		{
			int[] array = new int[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = this.names.IndexOfKey(keys[i]);
			}
			return this.SelectColumns(new ColumnSelection(keys, array));
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00016468 File Offset: 0x00014668
		public SoqlColumns Add(SoqlColumns added)
		{
			KeysBuilder keysBuilder = new KeysBuilder(this.names.Length + added.names.Length);
			keysBuilder.Union(this.names);
			keysBuilder.Union(added.names);
			SoqlExpression[] array = SoqlColumns.Concat<SoqlExpression>(this.expressions, added.expressions);
			IValueReference[] array2 = SoqlColumns.Concat<IValueReference>(this.types, added.types);
			bool[] array3 = SoqlColumns.Concat<bool>(this.isAggregate, added.isAggregate);
			return new SoqlColumns(keysBuilder.ToKeys(), array, array2, array3);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x000164F4 File Offset: 0x000146F4
		public bool TryUnion(SoqlColumns added, out SoqlColumns union)
		{
			KeysBuilder keysBuilder = new KeysBuilder(this.names.Length + added.names.Length);
			keysBuilder.Union(this.names);
			keysBuilder.Union(added.names);
			SoqlExpression[] array = SoqlColumns.Concat<SoqlExpression>(this.expressions, added.expressions);
			HashSet<int> hashSet = new HashSet<int>();
			SoqlExpression[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				ColumnAccessQueryExpression columnAccessQueryExpression = array2[i].Expression as ColumnAccessQueryExpression;
				if (columnAccessQueryExpression != null)
				{
					if (hashSet.Contains(columnAccessQueryExpression.Column))
					{
						union = null;
						return false;
					}
					hashSet.Add(columnAccessQueryExpression.Column);
				}
			}
			IValueReference[] array3 = SoqlColumns.Concat<IValueReference>(this.types, added.types);
			bool[] array4 = SoqlColumns.Concat<bool>(this.isAggregate, added.isAggregate);
			union = new SoqlColumns(keysBuilder.ToKeys(), array, array3, array4);
			return true;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000165D8 File Offset: 0x000147D8
		public bool TryGetName(string name, out SoqlExpression expression)
		{
			int num = this.names.IndexOfKey(name);
			if (num < 0)
			{
				expression = null;
				return false;
			}
			expression = this.expressions[num];
			return true;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001660C File Offset: 0x0001480C
		private static T[] Concat<T>(IList<T> source1, IList<T> source2)
		{
			T[] array = new T[source1.Count + source2.Count];
			for (int i = 0; i < source1.Count; i++)
			{
				array[i] = source1[i];
			}
			for (int j = 0; j < source2.Count; j++)
			{
				array[source1.Count + j] = source2[j];
			}
			return array;
		}

		// Token: 0x04000603 RID: 1539
		private readonly Keys names;

		// Token: 0x04000604 RID: 1540
		private readonly IList<SoqlExpression> expressions;

		// Token: 0x04000605 RID: 1541
		private readonly IValueReference[] types;

		// Token: 0x04000606 RID: 1542
		private readonly bool[] isAggregate;
	}
}
