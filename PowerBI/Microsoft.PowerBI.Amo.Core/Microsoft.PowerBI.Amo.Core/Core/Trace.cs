using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000EC RID: 236
	[XmlRoot(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	[Guid("D56131F6-EE90-4B3D-8B6D-9D6EF2E37AB0")]
	public abstract class Trace : MajorObject, IMajorObject, INamedComponent, IModelComponent, IComponent, IDisposable
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00033C18 File Offset: 0x00031E18
		[XmlIgnore]
		[Browsable(false)]
		public new Server Parent
		{
			get
			{
				return this.ParentOrNull;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00033C20 File Offset: 0x00031E20
		internal Server ParentOrException
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

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00033C4E File Offset: 0x00031E4E
		internal Server ParentOrNull
		{
			get
			{
				return (Server)base.Parent;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00033C5B File Offset: 0x00031E5B
		Database IMajorObject.ParentDatabase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00033C5E File Offset: 0x00031E5E
		Server IMajorObject.ParentServer
		{
			get
			{
				return this.Parent;
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00033C66 File Offset: 0x00031E66
		void IMajorObject.WriteRef(XmlWriter writer)
		{
			writer.WriteElementString("TraceID", base.ID);
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00033C79 File Offset: 0x00031E79
		Type IMajorObject.BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00033C81 File Offset: 0x00031E81
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00033C8C File Offset: 0x00031E8C
		string IMajorObject.Path
		{
			get
			{
				IMajorObject parent = this.Parent;
				return ((parent == null) ? string.Empty : parent.Path) + "<TraceID>" + base.ID + "</TraceID>";
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00033CC5 File Offset: 0x00031EC5
		IObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return this.GetObjectReference();
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00033CCD File Offset: 0x00031ECD
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return obj != null && this.Parent == obj;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00033CDD File Offset: 0x00031EDD
		internal override Type GetBaseType()
		{
			return typeof(Trace);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00033CE9 File Offset: 0x00031EE9
		internal Trace()
		{
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00033CF1 File Offset: 0x00031EF1
		internal Trace(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x020001AB RID: 427
		internal abstract class TraceBodyBase : MajorObject.MajorObjectBody
		{
			// Token: 0x0600133D RID: 4925 RVA: 0x000436E2 File Offset: 0x000418E2
			public TraceBodyBase(Trace owner)
				: base(owner)
			{
				this.LogFileAppend = true;
				this.LogFileSize = 0L;
				this.Audit = false;
				this.LogFileRollover = false;
				this.AutoRestart = false;
				this.StopTime = DateTime.MaxValue;
			}

			// Token: 0x040010E1 RID: 4321
			public string LogFileName;

			// Token: 0x040010E2 RID: 4322
			public bool LogFileAppend;

			// Token: 0x040010E3 RID: 4323
			public long LogFileSize;

			// Token: 0x040010E4 RID: 4324
			public bool Audit;

			// Token: 0x040010E5 RID: 4325
			public bool LogFileRollover;

			// Token: 0x040010E6 RID: 4326
			public bool AutoRestart;

			// Token: 0x040010E7 RID: 4327
			public DateTime StopTime;

			// Token: 0x040010E8 RID: 4328
			public XmlNode Filter;

			// Token: 0x040010E9 RID: 4329
			public XmlNode XEvent;
		}
	}
}
