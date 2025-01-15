using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200003C RID: 60
	[EditorBrowsable(1)]
	public sealed class AttendeeCollection : ComplexPropertyCollection<Attendee>
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000B337 File Offset: 0x0000A337
		internal AttendeeCollection()
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000B33F File Offset: 0x0000A33F
		public void Add(Attendee attendee)
		{
			base.InternalAdd(attendee);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B348 File Offset: 0x0000A348
		public Attendee Add(string smtpAddress)
		{
			Attendee attendee = new Attendee(smtpAddress);
			base.InternalAdd(attendee);
			return attendee;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B364 File Offset: 0x0000A364
		public Attendee Add(string name, string smtpAddress)
		{
			Attendee attendee = new Attendee(name, smtpAddress);
			base.InternalAdd(attendee);
			return attendee;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B381 File Offset: 0x0000A381
		public void Clear()
		{
			base.InternalClear();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B389 File Offset: 0x0000A389
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= base.Count)
			{
				throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
			}
			base.InternalRemoveAt(index);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B3B4 File Offset: 0x0000A3B4
		public bool Remove(Attendee attendee)
		{
			EwsUtilities.ValidateParam(attendee, "attendee");
			return base.InternalRemove(attendee);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B3C8 File Offset: 0x0000A3C8
		internal override Attendee CreateComplexProperty(string xmlElementName)
		{
			if (xmlElementName == "Attendee")
			{
				return new Attendee();
			}
			return null;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B3DE File Offset: 0x0000A3DE
		internal override Attendee CreateDefaultComplexProperty()
		{
			return new Attendee();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B3E5 File Offset: 0x0000A3E5
		internal override string GetCollectionItemXmlElementName(Attendee attendee)
		{
			return "Attendee";
		}
	}
}
