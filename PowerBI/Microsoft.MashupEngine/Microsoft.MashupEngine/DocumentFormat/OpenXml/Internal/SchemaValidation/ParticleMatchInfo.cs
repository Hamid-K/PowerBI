using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003117 RID: 12567
	[DebuggerDisplay("Match={Match}")]
	internal class ParticleMatchInfo
	{
		// Token: 0x0601B403 RID: 111619 RVA: 0x00373A06 File Offset: 0x00371C06
		internal ParticleMatchInfo()
		{
			this.Match = ParticleMatch.Nomatch;
		}

		// Token: 0x0601B404 RID: 111620 RVA: 0x00373A15 File Offset: 0x00371C15
		internal ParticleMatchInfo(OpenXmlElement startElement)
		{
			this.Match = ParticleMatch.Nomatch;
			this.StartElement = startElement;
		}

		// Token: 0x170098D8 RID: 39128
		// (get) Token: 0x0601B405 RID: 111621 RVA: 0x00373A2B File Offset: 0x00371C2B
		// (set) Token: 0x0601B406 RID: 111622 RVA: 0x00373A33 File Offset: 0x00371C33
		internal ParticleMatch Match { get; set; }

		// Token: 0x170098D9 RID: 39129
		// (get) Token: 0x0601B407 RID: 111623 RVA: 0x00373A3C File Offset: 0x00371C3C
		// (set) Token: 0x0601B408 RID: 111624 RVA: 0x00373A44 File Offset: 0x00371C44
		internal OpenXmlElement StartElement { get; private set; }

		// Token: 0x170098DA RID: 39130
		// (get) Token: 0x0601B409 RID: 111625 RVA: 0x00373A4D File Offset: 0x00371C4D
		// (set) Token: 0x0601B40A RID: 111626 RVA: 0x00373A55 File Offset: 0x00371C55
		internal OpenXmlElement LastMatchedElement { get; set; }

		// Token: 0x170098DB RID: 39131
		// (get) Token: 0x0601B40B RID: 111627 RVA: 0x00373A5E File Offset: 0x00371C5E
		// (set) Token: 0x0601B40C RID: 111628 RVA: 0x00373A66 File Offset: 0x00371C66
		internal string ErrorMessage { get; set; }

		// Token: 0x170098DC RID: 39132
		// (get) Token: 0x0601B40D RID: 111629 RVA: 0x00373A6F File Offset: 0x00371C6F
		// (set) Token: 0x0601B40E RID: 111630 RVA: 0x00373A77 File Offset: 0x00371C77
		internal ExpectedChildren ExpectedChildren { get; private set; }

		// Token: 0x0601B40F RID: 111631 RVA: 0x00373A80 File Offset: 0x00371C80
		internal void InitExpectedChildren()
		{
			if (this.ExpectedChildren == null)
			{
				this.ExpectedChildren = new ExpectedChildren();
				return;
			}
			this.ExpectedChildren.Clear();
		}

		// Token: 0x0601B410 RID: 111632 RVA: 0x00373AA4 File Offset: 0x00371CA4
		internal void SetExpectedChildren(ExpectedChildren expectedChildren)
		{
			if (expectedChildren == null || expectedChildren.Count == 0)
			{
				if (this.ExpectedChildren != null)
				{
					this.ExpectedChildren.Clear();
					return;
				}
			}
			else
			{
				if (this.ExpectedChildren == null)
				{
					this.ExpectedChildren = new ExpectedChildren();
				}
				this.ExpectedChildren.Clear();
				this.ExpectedChildren.Add(expectedChildren);
			}
		}

		// Token: 0x0601B411 RID: 111633 RVA: 0x00373AFA File Offset: 0x00371CFA
		internal void Reset(OpenXmlElement startElement)
		{
			this.StartElement = startElement;
			this.Match = ParticleMatch.Nomatch;
			this.LastMatchedElement = null;
			this.ErrorMessage = null;
			if (this.ExpectedChildren != null)
			{
				this.ExpectedChildren.Clear();
			}
		}
	}
}
