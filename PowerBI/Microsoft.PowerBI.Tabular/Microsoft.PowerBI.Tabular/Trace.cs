using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000DA RID: 218
	[XmlRoot(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	[Guid("6418C2E4-B678-46c3-8762-DC344F549317")]
	public sealed class Trace : Trace, IMajorObject, ITrace, ICloneable
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0006FD06 File Offset: 0x0006DF06
		Database IMajorObject.ParentDatabase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0006FD09 File Offset: 0x0006DF09
		Server IMajorObject.ParentServer
		{
			get
			{
				return this.Parent;
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0006FD11 File Offset: 0x0006DF11
		void IMajorObject.WriteRef(XmlWriter writer)
		{
			writer.WriteElementString("TraceID", base.ID);
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0006FD24 File Offset: 0x0006DF24
		Type IMajorObject.BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0006FD2C File Offset: 0x0006DF2C
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0006FD34 File Offset: 0x0006DF34
		string IMajorObject.Path
		{
			get
			{
				IMajorObject parent = this.Parent;
				return ((parent == null) ? string.Empty : parent.Path) + "<TraceID>" + base.ID + "</TraceID>";
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0006FD6D File Offset: 0x0006DF6D
		ObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return new ObjectReference
				{
					TraceID = base.ID
				};
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0006FD80 File Offset: 0x0006DF80
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return obj != null && this.Parent == obj;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0006FD90 File Offset: 0x0006DF90
		[XmlIgnore]
		[Browsable(false)]
		public new Server Parent
		{
			get
			{
				return this.ParentOrNull;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0006FD98 File Offset: 0x0006DF98
		internal new Server ParentOrException
		{
			get
			{
				IModelComponent parent = base.Parent;
				if (parent == null)
				{
					throw Utils.CreateParentMissingException(this, typeof(Server));
				}
				return (Server)parent;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0006FDC6 File Offset: 0x0006DFC6
		internal new Server ParentOrNull
		{
			get
			{
				return (Server)base.Parent;
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0006FDD3 File Offset: 0x0006DFD3
		internal override Type GetBaseType()
		{
			return typeof(Trace);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0006FDDF File Offset: 0x0006DFDF
		private protected override MajorObject.MajorObjectBody CreateBodyImpl()
		{
			return new Trace.TraceBody(this);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0006FDE7 File Offset: 0x0006DFE7
		object ICloneable.Clone()
		{
			return this.CopyTo(new Trace());
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0006FDF4 File Offset: 0x0006DFF4
		protected internal override MajorObject Clone(bool forceBodyLoading)
		{
			MajorObject majorObject = new Trace();
			this.CopyTo(majorObject, forceBodyLoading);
			return majorObject;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0006FE10 File Offset: 0x0006E010
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Trace trace = destination as Trace;
			if (trace == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
			if (!base.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			trace.LogFileName = this.LogFileName;
			trace.LogFileAppend = this.LogFileAppend;
			trace.LogFileSize = this.LogFileSize;
			trace.Audit = this.Audit;
			trace.LogFileRollover = this.LogFileRollover;
			trace.AutoRestart = this.AutoRestart;
			trace.StopTime = this.StopTime;
			trace.Filter = ((this.Filter == null) ? null : this.Filter.Clone());
			trace.XEvent = ((this.XEvent == null) ? null : this.XEvent.Clone());
			this.Events.CopyTo(trace.Events);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0006FEF6 File Offset: 0x0006E0F6
		public Trace()
		{
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0006FF09 File Offset: 0x0006E109
		public Trace(string name, string id)
			: base(name, id)
		{
			base.CreateBody();
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0006FF24 File Offset: 0x0006E124
		[XmlIgnore]
		[Browsable(false)]
		public bool IsStarted
		{
			get
			{
				return this.traceEventsReader != null && this.traceEventsReader.IsWorking;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000E4E RID: 3662 RVA: 0x0006FF3C File Offset: 0x0006E13C
		// (remove) Token: 0x06000E4F RID: 3663 RVA: 0x0006FF74 File Offset: 0x0006E174
		public event TraceEventHandler OnEvent;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000E50 RID: 3664 RVA: 0x0006FFAC File Offset: 0x0006E1AC
		// (remove) Token: 0x06000E51 RID: 3665 RVA: 0x0006FFE4 File Offset: 0x0006E1E4
		public event TraceStoppedEventHandler Stopped;

		// Token: 0x06000E52 RID: 3666 RVA: 0x00070019 File Offset: 0x0006E219
		private void TraceEventInvoker(object sender, TraceEventArgs e)
		{
			if (this.OnEvent != null)
			{
				this.OnEvent(sender, e);
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00070030 File Offset: 0x0006E230
		private void TraceStoppedEventInvoker(ITrace sender, TraceStoppedEventArgs e)
		{
			if (this.Stopped != null)
			{
				this.Stopped(sender, e);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00070047 File Offset: 0x0006E247
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x00070054 File Offset: 0x0006E254
		[XmlElement(IsNullable = false)]
		public string LogFileName
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().LogFileName;
			}
			set
			{
				Utils.CheckValidPath(value);
				base.GetBody<Trace.TraceBody>().LogFileName = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00070068 File Offset: 0x0006E268
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x00070075 File Offset: 0x0006E275
		[DefaultValue(true)]
		public bool LogFileAppend
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().LogFileAppend;
			}
			set
			{
				base.GetBody<Trace.TraceBody>().LogFileAppend = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00070083 File Offset: 0x0006E283
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x00070090 File Offset: 0x0006E290
		[DefaultValue(0L)]
		public long LogFileSize
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().LogFileSize;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentException(SR.Trace_LogFileSize_InvalidValue(value));
				}
				base.GetBody<Trace.TraceBody>().LogFileSize = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x000700AF File Offset: 0x0006E2AF
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x000700BC File Offset: 0x0006E2BC
		[DefaultValue(false)]
		public bool Audit
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().Audit;
			}
			set
			{
				base.GetBody<Trace.TraceBody>().Audit = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x000700CA File Offset: 0x0006E2CA
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x000700D7 File Offset: 0x0006E2D7
		[DefaultValue(false)]
		public bool LogFileRollover
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().LogFileRollover;
			}
			set
			{
				base.GetBody<Trace.TraceBody>().LogFileRollover = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x000700E5 File Offset: 0x0006E2E5
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x000700F2 File Offset: 0x0006E2F2
		[DefaultValue(false)]
		public bool AutoRestart
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().AutoRestart;
			}
			set
			{
				base.GetBody<Trace.TraceBody>().AutoRestart = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00070100 File Offset: 0x0006E300
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x0007010D File Offset: 0x0006E30D
		public DateTime StopTime
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().StopTime;
			}
			set
			{
				base.GetBody<Trace.TraceBody>().StopTime = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0007011B File Offset: 0x0006E31B
		[XmlArray]
		[XmlArrayItem("Event", typeof(TraceEvent))]
		[Browsable(false)]
		public new TraceEventCollection Events
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().Events;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00070128 File Offset: 0x0006E328
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x00070135 File Offset: 0x0006E335
		public XmlNode Filter
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().Filter;
			}
			set
			{
				base.GetBody<Trace.TraceBody>().Filter = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00070143 File Offset: 0x0006E343
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x00070150 File Offset: 0x0006E350
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300/300")]
		public XmlNode XEvent
		{
			get
			{
				return base.GetBody<Trace.TraceBody>().XEvent;
			}
			set
			{
				base.GetBody<Trace.TraceBody>().XEvent = value;
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00070160 File Offset: 0x0006E360
		public void Start()
		{
			this.ThrowIfDisposed();
			if (this.IsStarted)
			{
				throw new InvalidOperationException(SR.Trace_AlreadyStarted(base.Name));
			}
			Server parent = this.Parent;
			if (parent == null || !parent.Connected)
			{
				throw new InvalidOperationException(SR.Trace_NoConnectedParentServer(base.Name));
			}
			this.traceEventsReader.Start(this, new TraceEventHandler(this.TraceEventInvoker), new TraceStoppedEventHandler(this.TraceStoppedEventInvoker));
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000701D3 File Offset: 0x0006E3D3
		public void Stop()
		{
			this.ThrowIfDisposed();
			this.traceEventsReader.Stop();
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000701E6 File Offset: 0x0006E3E6
		public Trace CopyTo(Trace obj)
		{
			this.CopyTo(obj, true);
			return obj;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000701F1 File Offset: 0x0006E3F1
		public Trace Clone()
		{
			return this.CopyTo(new Trace());
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000701FE File Offset: 0x0006E3FE
		protected override void Dispose(bool disposing)
		{
			this.ThrowIfDisposed();
			if (disposing)
			{
				this.traceEventsReader.Dispose();
				this.traceEventsReader = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00070222 File Offset: 0x0006E422
		internal override bool IsSyntacticallyValidID(string newValue, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(newValue, type, out error);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0007022C File Offset: 0x0006E42C
		internal override bool IsValidName(string newValue, Type type, ModelType modelType, int compatibilityLevel, NamedComponentCollection namedComponentCollection, out string error)
		{
			return Utils.IsValidName(newValue, type, modelType, compatibilityLevel, namedComponentCollection, out error);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0007023C File Offset: 0x0006E43C
		private void ThrowIfDisposed()
		{
			if (this.traceEventsReader == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x040001A9 RID: 425
		internal static readonly int TraceColumnCount = 1 + Enum.GetValues(typeof(TraceColumn)).Cast<int>().Max();

		// Token: 0x040001AA RID: 426
		private TraceEventsReader traceEventsReader = new TraceEventsReader();

		// Token: 0x020002EE RID: 750
		private sealed class TraceBody : Trace.TraceBodyBase
		{
			// Token: 0x060023BE RID: 9150 RVA: 0x000E2BC7 File Offset: 0x000E0DC7
			public TraceBody(Trace owner)
				: base(owner)
			{
				this.Events = new TraceEventCollection();
			}

			// Token: 0x04000AC6 RID: 2758
			public TraceEventCollection Events;
		}
	}
}
