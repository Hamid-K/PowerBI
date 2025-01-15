using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x0200045D RID: 1117
	public abstract class FeatureNameCollection : IEnumerable<string>, IEnumerable
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x00086866 File Offset: 0x00084A66
		private FeatureNameCollection()
		{
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0008686E File Offset: 0x00084A6E
		public static FeatureNameCollection Create(string[] names)
		{
			return FeatureNameCollection.Create(Utils.Size<string>(names), names);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00086888 File Offset: 0x00084A88
		public static FeatureNameCollection Create(int count, string[] names = null)
		{
			Contracts.CheckParam(count >= 0, "count");
			int num = Math.Min(count, Utils.Size<string>(names));
			if (num >= 30)
			{
				int num2 = names.Take(num).Count((string x) => x != null);
				if (num2 < num / 2)
				{
					return new FeatureNameCollection.Sparse(count, names, num2);
				}
			}
			return new FeatureNameCollection.Dense(count, names);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0008692C File Offset: 0x00084B2C
		public static FeatureNameCollection Create(int count, Dictionary<int, string> map)
		{
			Contracts.CheckParam(count >= 0, "count");
			Contracts.CheckValue<Dictionary<int, string>>(map, "names");
			IEnumerable<KeyValuePair<int, string>> enumerable = map.Where((KeyValuePair<int, string> kvp) => 0 <= kvp.Key && kvp.Key < count && kvp.Value != null);
			int num = 0;
			int num2 = 0;
			foreach (KeyValuePair<int, string> keyValuePair in enumerable)
			{
				if (num <= keyValuePair.Key)
				{
					num = keyValuePair.Key + 1;
				}
				num2++;
			}
			string[] array2;
			if (num >= 30 && num2 < num / 2)
			{
				int[] array = new int[num2];
				array2 = new string[num2];
				int num3 = 0;
				foreach (KeyValuePair<int, string> keyValuePair2 in enumerable)
				{
					array[num3] = keyValuePair2.Key;
					array2[num3] = keyValuePair2.Value;
					num3++;
				}
				return new FeatureNameCollection.Sparse(count, array, array2);
			}
			array2 = new string[num];
			foreach (KeyValuePair<int, string> keyValuePair3 in enumerable)
			{
				array2[keyValuePair3.Key] = keyValuePair3.Value;
			}
			return new FeatureNameCollection.Dense(count, array2);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00086AB4 File Offset: 0x00084CB4
		public static FeatureNameCollection Create(RoleMappedSchema schema)
		{
			Contracts.CheckValue<RoleMappedSchema>(schema, "schema");
			Contracts.CheckParam(schema.Feature != null, "schema", "Cannot create feature name collection if we have no features");
			Contracts.CheckParam(schema.Feature.Type.ValueCount > 0, "schema", "Cannot create feature name collection if our features are not of known size");
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			int valueCount = schema.Feature.Type.ValueCount;
			if (MetadataUtils.HasSlotNames(schema.Schema, schema.Feature.Index, valueCount))
			{
				schema.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", schema.Feature.Index, ref vbuffer);
			}
			else
			{
				vbuffer = VBufferUtils.CreateEmpty<DvText>(valueCount);
			}
			string[] array = new string[vbuffer.Count];
			for (int i = 0; i < vbuffer.Count; i++)
			{
				array[i] = (vbuffer.Values[i].HasChars ? vbuffer.Values[i].ToString() : null);
			}
			if (vbuffer.IsDense)
			{
				return new FeatureNameCollection.Dense(array.Length, array);
			}
			int[] array2 = vbuffer.Indices;
			if (array2 == null)
			{
				array2 = new int[0];
			}
			else if (array2.Length != vbuffer.Count)
			{
				Array.Resize<int>(ref array2, vbuffer.Count);
			}
			return new FeatureNameCollection.Sparse(vbuffer.Length, array2, array);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00086C06 File Offset: 0x00084E06
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("FEATNAME", 65537U, 65537U, 65537U, "FeatureNamesExec", null);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00086C28 File Offset: 0x00084E28
		public static void Save(ModelSaveContext ctx, ref VBuffer<DvText> names)
		{
			ctx.CheckAtModel();
			ctx.SetVersionInfo(FeatureNameCollection.GetVersionInfo());
			ctx.Writer.Write(names.Length);
			if (names.IsDense)
			{
				ctx.Writer.Write(-1);
				for (int i = 0; i < names.Length; i++)
				{
					ctx.SaveStringOrNull(names.Values[i].ToString());
				}
				return;
			}
			ctx.Writer.Write(names.Count);
			for (int j = 0; j < names.Count; j++)
			{
				ctx.Writer.Write(names.Indices[j]);
			}
			for (int k = 0; k < names.Count; k++)
			{
				ctx.SaveStringOrNull(names.Values[k].ToString());
			}
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00086D00 File Offset: 0x00084F00
		public static FeatureNameCollection Create(ModelLoadContext ctx)
		{
			ctx.CheckAtModel();
			ctx.CheckVersionInfo(FeatureNameCollection.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num >= 0);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num2 >= -1);
			if (num2 < 0)
			{
				string[] array = new string[num];
				for (int i = 0; i < num; i++)
				{
					string text = ctx.LoadStringOrNull();
					array[i] = (string.IsNullOrEmpty(text) ? null : text);
				}
				return FeatureNameCollection.Create(num, array);
			}
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			int[] array2 = new int[num2];
			int num3 = -1;
			for (int j = 0; j < num2; j++)
			{
				array2[j] = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num3 < array2[j]);
				num3 = array2[j];
			}
			Contracts.CheckDecode(num3 < num);
			for (int k = 0; k < num2; k++)
			{
				string text2 = ctx.LoadStringOrNull();
				if (!string.IsNullOrEmpty(text2))
				{
					dictionary.Add(array2[k], text2);
				}
			}
			return FeatureNameCollection.Create(num, dictionary);
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600173A RID: 5946
		public abstract int Count { get; }

		// Token: 0x17000237 RID: 567
		public string this[int index]
		{
			get
			{
				return this.GetNameOrNull(index) ?? this.GetDefault(index);
			}
		}

		// Token: 0x0600173C RID: 5948
		public abstract string GetNameOrNull(int index);

		// Token: 0x0600173D RID: 5949
		public abstract IEnumerator<string> GetEnumerator();

		// Token: 0x0600173E RID: 5950 RVA: 0x00086E22 File Offset: 0x00085022
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00086E2A File Offset: 0x0008502A
		private string GetDefault(int index)
		{
			return string.Format("f{0}", index);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00086E3C File Offset: 0x0008503C
		public bool TryLookup(string name, out int index)
		{
			if (this._lookup == null)
			{
				this.BuildLookup();
			}
			if (this._lookup.TryGetValue(name, out index))
			{
				return true;
			}
			if (name.Length >= 2 && name[0] == 'f' && int.TryParse(name.Substring(1), out index) && 0 <= index && index < this.Count && name == this[index])
			{
				return true;
			}
			index = -1;
			return false;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00086EB4 File Offset: 0x000850B4
		private void BuildLookup()
		{
			if (this._lock == null)
			{
				Interlocked.CompareExchange(ref this._lock, new object(), null);
			}
			lock (this._lock)
			{
				if (this._lookup == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					this.PopulateLookup(dictionary);
					this._lookup = dictionary;
				}
			}
		}

		// Token: 0x06001742 RID: 5954
		protected abstract void PopulateLookup(Dictionary<string, int> lookup);

		// Token: 0x04000E32 RID: 3634
		private const string DefaultFmt = "f{0}";

		// Token: 0x04000E33 RID: 3635
		public const string LoaderSignature = "FeatureNamesExec";

		// Token: 0x04000E34 RID: 3636
		private volatile Dictionary<string, int> _lookup;

		// Token: 0x04000E35 RID: 3637
		private volatile object _lock;

		// Token: 0x0200045E RID: 1118
		private sealed class Dense : FeatureNameCollection
		{
			// Token: 0x06001744 RID: 5956 RVA: 0x00086F30 File Offset: 0x00085130
			public Dense(int count, string[] names)
			{
				this._count = count;
				int num = Math.Min(Utils.Size<string>(names), count);
				this._names = new string[num];
				if (num > 0)
				{
					Array.Copy(names, this._names, num);
				}
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06001745 RID: 5957 RVA: 0x00086F74 File Offset: 0x00085174
			public override int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x06001746 RID: 5958 RVA: 0x00086F7C File Offset: 0x0008517C
			public override string GetNameOrNull(int index)
			{
				Contracts.CheckParam(0 <= index && index < this._count, "index");
				if (index >= this._names.Length)
				{
					return null;
				}
				return this._names[index];
			}

			// Token: 0x06001747 RID: 5959 RVA: 0x000870D4 File Offset: 0x000852D4
			public override IEnumerator<string> GetEnumerator()
			{
				for (int i = 0; i < this._names.Length; i++)
				{
					yield return this._names[i] ?? base.GetDefault(i);
				}
				for (int j = this._names.Length; j < this._count; j++)
				{
					yield return base.GetDefault(j);
				}
				yield break;
			}

			// Token: 0x06001748 RID: 5960 RVA: 0x000870F0 File Offset: 0x000852F0
			protected override void PopulateLookup(Dictionary<string, int> lookup)
			{
				for (int i = 0; i < this._names.Length; i++)
				{
					string text = this._names[i];
					if (text != null)
					{
						lookup[text] = i;
					}
				}
			}

			// Token: 0x04000E37 RID: 3639
			private readonly int _count;

			// Token: 0x04000E38 RID: 3640
			private readonly string[] _names;
		}

		// Token: 0x0200045F RID: 1119
		private sealed class Sparse : FeatureNameCollection
		{
			// Token: 0x06001749 RID: 5961 RVA: 0x00087124 File Offset: 0x00085324
			public Sparse(int count, string[] names, int cnn)
			{
				this._count = count;
				int num = Math.Min(names.Length, count);
				this._names = new string[cnn];
				this._indices = new int[cnn];
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					string text = names[i];
					if (text != null)
					{
						this._names[num2] = text;
						this._indices[num2] = i;
						num2++;
					}
				}
			}

			// Token: 0x0600174A RID: 5962 RVA: 0x0008718B File Offset: 0x0008538B
			public Sparse(int count, int[] indices, string[] names)
			{
				this._count = count;
				this._indices = indices;
				this._names = names;
			}

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x0600174B RID: 5963 RVA: 0x000871A8 File Offset: 0x000853A8
			public override int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x0600174C RID: 5964 RVA: 0x000871B0 File Offset: 0x000853B0
			public override string GetNameOrNull(int index)
			{
				Contracts.CheckParam(0 <= index && index < this._count, "index");
				int num = this._ivPrev;
				if (num < this._indices.Length && this._indices[num] < index)
				{
					if (++num < this._indices.Length && this._indices[num] < index)
					{
						num = Utils.FindIndexSorted(this._indices, num + 1, this._indices.Length, index);
					}
				}
				else if (num > 0 && this._indices[num - 1] >= index && --num > 0 && this._indices[num - 1] >= index)
				{
					num = Utils.FindIndexSorted(this._indices, 0, num - 1, index);
				}
				this._ivPrev = num;
				if (num < this._names.Length && this._indices[num] == index)
				{
					return this._names[num];
				}
				return null;
			}

			// Token: 0x0600174D RID: 5965 RVA: 0x000873AC File Offset: 0x000855AC
			public override IEnumerator<string> GetEnumerator()
			{
				int ii = 0;
				for (int i = 0; i < this._count; i++)
				{
					if (ii < this._indices.Length && this._indices[ii] == i)
					{
						string[] names = this._names;
						int num;
						ii = (num = ii) + 1;
						yield return names[num];
					}
					else
					{
						yield return base.GetDefault(i);
					}
				}
				yield break;
			}

			// Token: 0x0600174E RID: 5966 RVA: 0x000873C8 File Offset: 0x000855C8
			protected override void PopulateLookup(Dictionary<string, int> lookup)
			{
				for (int i = 0; i < this._names.Length; i++)
				{
					string text = this._names[i];
					lookup[text] = this._indices[i];
				}
			}

			// Token: 0x04000E39 RID: 3641
			private readonly int _count;

			// Token: 0x04000E3A RID: 3642
			private readonly string[] _names;

			// Token: 0x04000E3B RID: 3643
			private readonly int[] _indices;

			// Token: 0x04000E3C RID: 3644
			private volatile int _ivPrev;
		}
	}
}
