using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000B6 RID: 182
	[Guid("557892C9-D64E-4db1-961B-5942124B4906")]
	public class RoleMember : ICloneable
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x000288C2 File Offset: 0x00026AC2
		public RoleMember()
		{
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000288CA File Offset: 0x00026ACA
		public RoleMember(string name)
		{
			this.Name = name;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000288D9 File Offset: 0x00026AD9
		public RoleMember(string name, string sid)
		{
			this.Name = name;
			this.Sid = sid;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000288EF File Offset: 0x00026AEF
		object ICloneable.Clone()
		{
			return new RoleMember(this.name, this.sid);
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00028902 File Offset: 0x00026B02
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0002890A File Offset: 0x00026B0A
		[XmlElement(IsNullable = false)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = Utils.Trim(value);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00028918 File Offset: 0x00026B18
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00028920 File Offset: 0x00026B20
		[XmlElement(IsNullable = false)]
		public string Sid
		{
			get
			{
				return this.sid;
			}
			set
			{
				this.sid = Utils.Trim(value);
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002892E File Offset: 0x00026B2E
		public RoleMember CopyTo(RoleMember obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (obj == this)
			{
				throw new InvalidOperationException(SR.Copy_DestinationIsSelf);
			}
			obj.Name = this.Name;
			obj.Sid = this.Sid;
			return obj;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00028966 File Offset: 0x00026B66
		public RoleMember Clone()
		{
			return new RoleMember(this.name, this.sid);
		}

		// Token: 0x040004E7 RID: 1255
		private string name;

		// Token: 0x040004E8 RID: 1256
		private string sid;
	}
}
