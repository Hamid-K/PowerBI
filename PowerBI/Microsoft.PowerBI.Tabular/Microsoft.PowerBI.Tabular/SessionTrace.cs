using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D9 RID: 217
	[Guid("84CF5C3D-7BD9-40a5-A4AC-18A1E2A32E15")]
	public sealed class SessionTrace : ITrace, IDisposable
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x0006FAF8 File Offset: 0x0006DCF8
		internal SessionTrace(Server parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			this.eventsReader = new TraceEventsReader();
			this.parent = parent;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0006FB20 File Offset: 0x0006DD20
		[XmlIgnore]
		[Browsable(false)]
		public Server Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0006FB28 File Offset: 0x0006DD28
		[XmlIgnore]
		[Browsable(false)]
		public bool IsStarted
		{
			get
			{
				return this.eventsReader != null && this.eventsReader.IsWorking;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000E31 RID: 3633 RVA: 0x0006FB40 File Offset: 0x0006DD40
		// (remove) Token: 0x06000E32 RID: 3634 RVA: 0x0006FB78 File Offset: 0x0006DD78
		public event TraceEventHandler OnEvent;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000E33 RID: 3635 RVA: 0x0006FBB0 File Offset: 0x0006DDB0
		// (remove) Token: 0x06000E34 RID: 3636 RVA: 0x0006FBE8 File Offset: 0x0006DDE8
		public event TraceStoppedEventHandler Stopped;

		// Token: 0x06000E35 RID: 3637 RVA: 0x0006FC20 File Offset: 0x0006DE20
		public void Start()
		{
			this.ThrowIfDisposed();
			if (this.IsStarted)
			{
				throw new InvalidOperationException(SR.SessionTrace_AlreadyStarted);
			}
			if (this.parent == null || !this.parent.Connected)
			{
				throw new InvalidOperationException(SR.SessionTrace_NoConnectedParentServer);
			}
			this.eventsReader.Start(this, new TraceEventHandler(this.TraceEventInvoker), new TraceStoppedEventHandler(this.TraceStoppedEventInvoker));
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0006FC8A File Offset: 0x0006DE8A
		public void Stop()
		{
			this.ThrowIfDisposed();
			this.eventsReader.Stop();
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0006FC9D File Offset: 0x0006DE9D
		public void Dispose()
		{
			this.ThrowIfDisposed();
			this.eventsReader.Dispose();
			this.eventsReader = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0006FCBD File Offset: 0x0006DEBD
		private void ThrowIfDisposed()
		{
			if (this.eventsReader == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0006FCD8 File Offset: 0x0006DED8
		private void TraceEventInvoker(object sender, TraceEventArgs e)
		{
			if (this.OnEvent != null)
			{
				this.OnEvent(sender, e);
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0006FCEF File Offset: 0x0006DEEF
		private void TraceStoppedEventInvoker(ITrace sender, TraceStoppedEventArgs e)
		{
			if (this.Stopped != null)
			{
				this.Stopped(sender, e);
			}
		}

		// Token: 0x040001A5 RID: 421
		private TraceEventsReader eventsReader;

		// Token: 0x040001A6 RID: 422
		private Server parent;
	}
}
