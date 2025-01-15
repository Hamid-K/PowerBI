using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200005B RID: 91
	internal abstract class StreamingDsrWriterWrapperBase
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000554A File Offset: 0x0000374A
		protected StreamingDsrWriterWrapperBase()
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005552 File Offset: 0x00003752
		protected StreamingDsrWriterWrapperBase(IStreamingStructureEncodedWriter structureWriter, DsrNames dsrNames)
		{
			this.Writer = structureWriter;
			this.DsrNames = dsrNames;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00005568 File Offset: 0x00003768
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00005570 File Offset: 0x00003770
		public IStreamingStructureEncodedWriter Writer { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00005579 File Offset: 0x00003779
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00005581 File Offset: 0x00003781
		internal DsrNames DsrNames { get; private set; }

		// Token: 0x060001CC RID: 460 RVA: 0x0000558A File Offset: 0x0000378A
		protected T CreateAndBeginChild<T>(ref T childWriter) where T : StreamingDsrWriterWrapperBase, new()
		{
			if (childWriter == null)
			{
				childWriter = StreamingDsrWriterWrapperBase.CreateWriter<T>(this.Writer, this.DsrNames);
			}
			childWriter.Begin();
			return childWriter;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000055C2 File Offset: 0x000037C2
		protected CollectionWriter<T> CreateAndBeginChild<T>(ref CollectionWriter<T> childWriter) where T : StreamingDsrWriterWrapperBase, new()
		{
			return this.CreateAndBeginCollectionWriter<T>(ref childWriter, false);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000055CC File Offset: 0x000037CC
		protected CollectionWriter<T> CreateAndBeginCollectionWriter<T>(ref CollectionWriter<T> childWriter, bool inlineInParentWriter) where T : StreamingDsrWriterWrapperBase, new()
		{
			if (childWriter == null)
			{
				childWriter = new CollectionWriter<T>(this, inlineInParentWriter);
			}
			childWriter.Begin();
			return childWriter;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000055E4 File Offset: 0x000037E4
		internal static T CreateWriter<T>(IStreamingStructureEncodedWriter structureWriter, DsrNames dsrNames) where T : StreamingDsrWriterWrapperBase, new()
		{
			T t = new T();
			t.Writer = structureWriter;
			t.DsrNames = dsrNames;
			return t;
		}

		// Token: 0x060001D0 RID: 464
		internal abstract void Begin();

		// Token: 0x060001D1 RID: 465
		internal abstract void End();
	}
}
