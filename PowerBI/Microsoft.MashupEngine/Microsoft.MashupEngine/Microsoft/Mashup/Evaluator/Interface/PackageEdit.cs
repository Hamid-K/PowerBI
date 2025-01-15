using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E1B RID: 7707
	public struct PackageEdit
	{
		// Token: 0x0600BDFD RID: 48637 RVA: 0x00267367 File Offset: 0x00265567
		public PackageEdit(string section, int offset, int length, SegmentedString text)
		{
			this.section = section;
			this.offset = offset;
			this.length = length;
			this.text = text;
		}

		// Token: 0x17002EBD RID: 11965
		// (get) Token: 0x0600BDFE RID: 48638 RVA: 0x00267386 File Offset: 0x00265586
		public string Section
		{
			get
			{
				return this.section;
			}
		}

		// Token: 0x17002EBE RID: 11966
		// (get) Token: 0x0600BDFF RID: 48639 RVA: 0x0026738E File Offset: 0x0026558E
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17002EBF RID: 11967
		// (get) Token: 0x0600BE00 RID: 48640 RVA: 0x00267396 File Offset: 0x00265596
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17002EC0 RID: 11968
		// (get) Token: 0x0600BE01 RID: 48641 RVA: 0x0026739E File Offset: 0x0026559E
		public SegmentedString Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x040060DC RID: 24796
		private string section;

		// Token: 0x040060DD RID: 24797
		private int offset;

		// Token: 0x040060DE RID: 24798
		private int length;

		// Token: 0x040060DF RID: 24799
		private SegmentedString text;
	}
}
