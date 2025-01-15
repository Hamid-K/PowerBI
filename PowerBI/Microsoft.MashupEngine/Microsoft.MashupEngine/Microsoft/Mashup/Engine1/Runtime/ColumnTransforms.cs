using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200163F RID: 5695
	public class ColumnTransforms
	{
		// Token: 0x06008F5E RID: 36702 RVA: 0x001DD8D8 File Offset: 0x001DBAD8
		public ColumnTransforms(IDictionary<int, ColumnTransform> transforms)
		{
			this.dictionary = transforms;
		}

		// Token: 0x1700258C RID: 9612
		// (get) Token: 0x06008F5F RID: 36703 RVA: 0x001DD8E7 File Offset: 0x001DBAE7
		public IDictionary<int, ColumnTransform> Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		// Token: 0x06008F60 RID: 36704 RVA: 0x001DD8F0 File Offset: 0x001DBAF0
		public ColumnTransforms SelectColumns(ColumnSelection columnSelection)
		{
			Dictionary<int, ColumnTransform> dictionary = new Dictionary<int, ColumnTransform>(Math.Min(columnSelection.Keys.Length, this.dictionary.Count));
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				ColumnTransform columnTransform;
				if (this.dictionary.TryGetValue(columnSelection.GetColumn(i), out columnTransform))
				{
					dictionary.Add(i, columnTransform);
				}
			}
			return new ColumnTransforms(dictionary);
		}

		// Token: 0x06008F61 RID: 36705 RVA: 0x001DD958 File Offset: 0x001DBB58
		public ColumnTransforms TransformColumns(ColumnTransforms transforms)
		{
			Dictionary<int, ColumnTransform> dictionary = new Dictionary<int, ColumnTransform>(this.dictionary);
			foreach (KeyValuePair<int, ColumnTransform> keyValuePair in transforms.dictionary)
			{
				int key = keyValuePair.Key;
				ColumnTransform value = keyValuePair.Value;
				ColumnTransform columnTransform;
				if (dictionary.TryGetValue(key, out columnTransform))
				{
					value = new ColumnTransform(new ColumnTransforms.ComposeFunctionValue(columnTransform.Function, value.Function), value.Type);
					dictionary.Remove(key);
				}
				dictionary.Add(key, value);
			}
			return new ColumnTransforms(dictionary);
		}

		// Token: 0x04004D8D RID: 19853
		public static readonly ColumnTransforms None = new ColumnTransforms(new Dictionary<int, ColumnTransform>());

		// Token: 0x04004D8E RID: 19854
		private readonly IDictionary<int, ColumnTransform> dictionary;

		// Token: 0x02001640 RID: 5696
		private class ComposeFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06008F63 RID: 36707 RVA: 0x001DDA11 File Offset: 0x001DBC11
			public ComposeFunctionValue(FunctionValue inner, FunctionValue outer)
			{
				this.inner = inner;
				this.outer = outer;
			}

			// Token: 0x06008F64 RID: 36708 RVA: 0x001DDA27 File Offset: 0x001DBC27
			public override Value Invoke(Value arg0)
			{
				return this.outer.Invoke(this.inner.Invoke(arg0));
			}

			// Token: 0x04004D8F RID: 19855
			private readonly FunctionValue inner;

			// Token: 0x04004D90 RID: 19856
			private readonly FunctionValue outer;
		}
	}
}
