using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003DD RID: 989
	public sealed class GroupExpressions : ArrayList
	{
		// Token: 0x170008D6 RID: 2262
		[XmlArrayItem("GroupExpression", typeof(Expression))]
		public Expression this[int index]
		{
			get
			{
				return (Expression)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0007E8B8 File Offset: 0x0007CAB8
		internal bool Contains(Field field)
		{
			Regex regex = new Regex("^=?Fields!" + field.Name + ".Value$", RegexOptions.IgnoreCase);
			for (int i = 0; i < this.Count; i++)
			{
				if (regex.IsMatch(this[i].String))
				{
					return true;
				}
			}
			return false;
		}
	}
}
