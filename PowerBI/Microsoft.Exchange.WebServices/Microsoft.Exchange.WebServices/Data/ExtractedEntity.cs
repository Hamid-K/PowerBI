using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200002A RID: 42
	public abstract class ExtractedEntity : ComplexProperty
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00009397 File Offset: 0x00008397
		internal ExtractedEntity()
		{
			base.Namespace = XmlNamespace.Types;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000093A6 File Offset: 0x000083A6
		// (set) Token: 0x060001FF RID: 511 RVA: 0x000093AE File Offset: 0x000083AE
		public EmailPosition Position { get; internal set; }

		// Token: 0x06000200 RID: 512 RVA: 0x000093B8 File Offset: 0x000083B8
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null && localName == "Position")
			{
				string text = reader.ReadElementValue();
				if (!string.IsNullOrEmpty(text))
				{
					this.Position = EwsUtilities.Parse<EmailPosition>(text);
				}
				return true;
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
