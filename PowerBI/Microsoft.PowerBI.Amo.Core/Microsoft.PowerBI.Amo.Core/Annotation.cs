using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000070 RID: 112
	[Guid("FEBF233D-E8CF-4eaf-8B2A-ED7E1C9E183D")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	public sealed class Annotation : ICloneable
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x000225BB File Offset: 0x000207BB
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000225C3 File Offset: 0x000207C3
		public Annotation()
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000225D2 File Offset: 0x000207D2
		public Annotation(string name)
		{
			this.Name = name;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000225E8 File Offset: 0x000207E8
		public Annotation(string name, string value)
		{
			this.Name = name;
			this.TextValue = value;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00022605 File Offset: 0x00020805
		public Annotation(string name, XmlNode value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00022622 File Offset: 0x00020822
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0002264C File Offset: 0x0002084C
		[XmlIgnore]
		internal string TextValue
		{
			get
			{
				if (this.Value == null)
				{
					return null;
				}
				XmlText xmlText = this.Value as XmlText;
				if (xmlText == null)
				{
					throw new InvalidOperationException(SR.InvalidAnnotationValue);
				}
				return xmlText.Value;
			}
			set
			{
				XmlDocument xmlDocument = new XmlDocument();
				this.Value = xmlDocument.CreateTextNode(value);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0002266C File Offset: 0x0002086C
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00022674 File Offset: 0x00020874
		[XmlElement(IsNullable = false)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.name != value)
				{
					if (this.owningCollection != null)
					{
						throw new InvalidOperationException(SR.PropertyCannotBeChangedForObjectInCollection("Name", typeof(Annotation).Name));
					}
					this.name = Utils.Trim(value);
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x000226C2 File Offset: 0x000208C2
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x000226CA File Offset: 0x000208CA
		[DefaultValue(AnnotationVisibility.None)]
		public AnnotationVisibility Visibility
		{
			get
			{
				return this.visibility;
			}
			set
			{
				this.visibility = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000226D3 File Offset: 0x000208D3
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x000226DB File Offset: 0x000208DB
		[XmlElement(IsNullable = false)]
		public XmlNode Value
		{
			get
			{
				return this.theValue;
			}
			set
			{
				this.theValue = value;
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000226E4 File Offset: 0x000208E4
		public Annotation Clone()
		{
			Annotation annotation = new Annotation();
			annotation.Name = this.Name;
			annotation.Visibility = this.Visibility;
			if (this.theValue != null)
			{
				annotation.theValue = this.theValue.Clone();
			}
			return annotation;
		}

		// Token: 0x0400040F RID: 1039
		internal AnnotationCollection owningCollection;

		// Token: 0x04000410 RID: 1040
		private string name;

		// Token: 0x04000411 RID: 1041
		private XmlNode theValue;

		// Token: 0x04000412 RID: 1042
		private AnnotationVisibility visibility = AnnotationVisibility.None;
	}
}
