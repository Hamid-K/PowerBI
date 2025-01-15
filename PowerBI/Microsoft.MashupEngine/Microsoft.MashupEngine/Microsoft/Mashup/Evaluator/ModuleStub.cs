using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D01 RID: 7425
	public sealed class ModuleStub : IModule
	{
		// Token: 0x0600B93A RID: 47418 RVA: 0x00258875 File Offset: 0x00256A75
		public ModuleStub(BinaryReader reader)
		{
			this.name = reader.ReadString();
			this.version = reader.ReadNullableString();
			this.keys = new ModuleStub.Keys(reader);
			this.dataSources = EmptyArray<ResourceKindInfo>.Instance;
		}

		// Token: 0x0600B93B RID: 47419 RVA: 0x002588AC File Offset: 0x00256AAC
		public ModuleStub(string name, string version, params string[] keys)
		{
			this.name = name;
			this.version = version;
			this.keys = new ModuleStub.Keys(keys);
			this.dataSources = EmptyArray<ResourceKindInfo>.Instance;
		}

		// Token: 0x0600B93C RID: 47420 RVA: 0x002588D9 File Offset: 0x00256AD9
		public static void Write(BinaryWriter writer, IModule module)
		{
			writer.WriteString(module.Name);
			writer.WriteNullableString(module.Version);
			ModuleStub.Keys.Write(writer, module.Exports);
		}

		// Token: 0x17002DD2 RID: 11730
		// (get) Token: 0x0600B93D RID: 47421 RVA: 0x002588FF File Offset: 0x00256AFF
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002DD3 RID: 11731
		// (get) Token: 0x0600B93E RID: 47422 RVA: 0x00258907 File Offset: 0x00256B07
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17002DD4 RID: 11732
		// (get) Token: 0x0600B93F RID: 47423 RVA: 0x0025890F File Offset: 0x00256B0F
		public IKeys Exports
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x17002DD5 RID: 11733
		// (get) Token: 0x0600B940 RID: 47424 RVA: 0x00258917 File Offset: 0x00256B17
		// (set) Token: 0x0600B941 RID: 47425 RVA: 0x00258920 File Offset: 0x00256B20
		public IRecordValue Metadata
		{
			get
			{
				return this.metadata;
			}
			set
			{
				IValue value2;
				if (value != null && value.TryGetValue("Version", out value2) && value2.IsText)
				{
					this.version = value2.AsString;
				}
				this.metadata = value;
			}
		}

		// Token: 0x17002DD6 RID: 11734
		// (get) Token: 0x0600B942 RID: 47426 RVA: 0x0025895A File Offset: 0x00256B5A
		// (set) Token: 0x0600B943 RID: 47427 RVA: 0x00258962 File Offset: 0x00256B62
		public ResourceKindInfo[] DataSources
		{
			get
			{
				return this.dataSources;
			}
			set
			{
				this.dataSources = value;
			}
		}

		// Token: 0x17002DD7 RID: 11735
		// (get) Token: 0x0600B944 RID: 47428 RVA: 0x0025896B File Offset: 0x00256B6B
		// (set) Token: 0x0600B945 RID: 47429 RVA: 0x00258973 File Offset: 0x00256B73
		public ResourceKindInfo DynamicModuleDataSource { get; set; }

		// Token: 0x04005E48 RID: 24136
		private readonly string name;

		// Token: 0x04005E49 RID: 24137
		private readonly ModuleStub.Keys keys;

		// Token: 0x04005E4A RID: 24138
		private ResourceKindInfo[] dataSources;

		// Token: 0x04005E4B RID: 24139
		private IRecordValue metadata;

		// Token: 0x04005E4C RID: 24140
		private string version;

		// Token: 0x02001D02 RID: 7426
		private class Keys : IKeys, IEnumerable<string>, IEnumerable
		{
			// Token: 0x0600B946 RID: 47430 RVA: 0x0025897C File Offset: 0x00256B7C
			public Keys(BinaryReader reader)
			{
				this.keys = reader.ReadArray(new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadString));
			}

			// Token: 0x0600B947 RID: 47431 RVA: 0x0025899C File Offset: 0x00256B9C
			public Keys(string[] keys)
			{
				this.keys = keys;
			}

			// Token: 0x0600B948 RID: 47432 RVA: 0x002589AB File Offset: 0x00256BAB
			public static void Write(BinaryWriter writer, IKeys keys)
			{
				writer.WriteArray(keys.ToArray<string>(), new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteString));
			}

			// Token: 0x17002DD8 RID: 11736
			// (get) Token: 0x0600B949 RID: 47433 RVA: 0x002589C5 File Offset: 0x00256BC5
			public int Length
			{
				get
				{
					return this.keys.Length;
				}
			}

			// Token: 0x17002DD9 RID: 11737
			public string this[int index]
			{
				get
				{
					return this.keys[index];
				}
			}

			// Token: 0x0600B94B RID: 47435 RVA: 0x002589DC File Offset: 0x00256BDC
			public bool TryGetIndex(string key, out int index)
			{
				index = Array.FindIndex<string>(this.keys, (string k) => string.Equals(k, key));
				return index >= 0;
			}

			// Token: 0x0600B94C RID: 47436 RVA: 0x00258A17 File Offset: 0x00256C17
			public IEnumerator<string> GetEnumerator()
			{
				return ((IEnumerable<string>)this.keys).GetEnumerator();
			}

			// Token: 0x0600B94D RID: 47437 RVA: 0x00258A24 File Offset: 0x00256C24
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.keys.GetEnumerator();
			}

			// Token: 0x04005E4E RID: 24142
			private readonly string[] keys;
		}
	}
}
