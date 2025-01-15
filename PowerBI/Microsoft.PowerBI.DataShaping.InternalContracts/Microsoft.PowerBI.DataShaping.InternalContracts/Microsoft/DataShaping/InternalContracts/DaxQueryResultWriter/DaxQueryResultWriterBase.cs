using System;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.InternalContracts.DaxQueryResultWriter
{
	// Token: 0x02000036 RID: 54
	internal abstract class DaxQueryResultWriterBase
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000444B File Offset: 0x0000264B
		protected DaxQueryResultWriterBase()
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004453 File Offset: 0x00002653
		protected DaxQueryResultWriterBase(IStreamingStructureWriter structureWriter, DaxQueryResultWriterSettings settings)
		{
			this.Writer = structureWriter;
			this.Settings = settings;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004469 File Offset: 0x00002669
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00004471 File Offset: 0x00002671
		public IStreamingStructureWriter Writer { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000447A File Offset: 0x0000267A
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00004482 File Offset: 0x00002682
		internal DaxQueryResultWriterSettings Settings { get; private set; }

		// Token: 0x06000138 RID: 312 RVA: 0x0000448B File Offset: 0x0000268B
		protected T CreateChild<T>(ref T childWriter) where T : DaxQueryResultWriterBase, new()
		{
			if (childWriter == null)
			{
				childWriter = DaxQueryResultWriterBase.CreateWriter<T>(this.Writer, this.Settings);
			}
			return childWriter;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000044B7 File Offset: 0x000026B7
		protected T CreateAndBeginChild<T>(ref T childWriter) where T : DaxQueryResultWriterBase, new()
		{
			this.CreateChild<T>(ref childWriter);
			childWriter.Begin();
			return childWriter;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000044D3 File Offset: 0x000026D3
		protected DaxQueryResultCollectionWriter<T> CreateAndBeginChild<T>(ref DaxQueryResultCollectionWriter<T> childWriter) where T : DaxQueryResultWriterBase, new()
		{
			return this.CreateAndBeginCollectionWriter<T>(ref childWriter);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000044DC File Offset: 0x000026DC
		protected DaxQueryResultCollectionWriter<T> CreateAndBeginCollectionWriter<T>(ref DaxQueryResultCollectionWriter<T> childWriter) where T : DaxQueryResultWriterBase, new()
		{
			if (childWriter == null)
			{
				childWriter = new DaxQueryResultCollectionWriter<T>(this);
			}
			childWriter.Begin();
			return childWriter;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000044F3 File Offset: 0x000026F3
		internal static T CreateWriter<T>(IStreamingStructureWriter structureWriter, DaxQueryResultWriterSettings settings) where T : DaxQueryResultWriterBase, new()
		{
			T t = new T();
			t.Writer = structureWriter;
			t.Settings = settings;
			return t;
		}

		// Token: 0x0600013D RID: 317
		internal abstract void Begin();

		// Token: 0x0600013E RID: 318
		internal abstract void End();
	}
}
