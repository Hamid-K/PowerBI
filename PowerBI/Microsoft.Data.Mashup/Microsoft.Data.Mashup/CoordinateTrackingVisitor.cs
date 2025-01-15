using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000007 RID: 7
	internal class CoordinateTrackingVisitor : ReadOnlyAstVisitor
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002998 File Offset: 0x00000B98
		public CoordinateTrackingVisitor(MashupPartitionCoordinateType coordinateType)
		{
			this.maxCoordinateDepth = (int)coordinateType;
			if (this.maxCoordinateDepth > 0)
			{
				this.coordinatePath = new Stack<string>(this.maxCoordinateDepth);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000029C4 File Offset: 0x00000BC4
		protected MashupPartitionCoordinate CurrentCoordinate
		{
			get
			{
				if (this.coordinatePath == null || this.coordinatePath.Count == 0)
				{
					return MashupPartitionCoordinate.Empty;
				}
				string[] array = new string[this.coordinatePath.Count];
				int num = this.coordinatePath.Count;
				foreach (string text in this.coordinatePath)
				{
					num--;
					array[num] = text;
				}
				return new MashupPartitionCoordinate(array);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A58 File Offset: 0x00000C58
		protected override void VisitModuleDocument(ISectionDocument document)
		{
			using (this.NewScope(document.Section.SectionName))
			{
				base.VisitModuleDocument(document);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002AA4 File Offset: 0x00000CA4
		protected override void VisitExpressionDocument(IExpressionDocument document)
		{
			using (this.NewScope(string.Empty))
			{
				base.VisitExpressionDocument(document);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002AE4 File Offset: 0x00000CE4
		protected override void VisitModuleMember(ISectionMember moduleMember)
		{
			using (this.NewScope(moduleMember.Name))
			{
				base.VisitModuleMember(moduleMember);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B2C File Offset: 0x00000D2C
		protected override void VisitLet(ILetExpression let)
		{
			for (int i = 0; i < let.Variables.Count; i++)
			{
				using (this.NewScope(let.Variables[i].Name))
				{
					this.VisitInitializer(let.Variables[i]);
				}
			}
			this.VisitExpression(let.Expression);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002BB0 File Offset: 0x00000DB0
		protected CoordinateTrackingVisitor.ExitCoordinateScope NewScope(string name)
		{
			if (this.coordinatePath == null || this.coordinatePath.Count >= this.maxCoordinateDepth)
			{
				return new CoordinateTrackingVisitor.ExitCoordinateScope(null);
			}
			this.coordinatePath.Push(name ?? string.Empty);
			return new CoordinateTrackingVisitor.ExitCoordinateScope(this);
		}

		// Token: 0x04000009 RID: 9
		private readonly int maxCoordinateDepth;

		// Token: 0x0400000A RID: 10
		private readonly Stack<string> coordinatePath;

		// Token: 0x02000055 RID: 85
		protected struct ExitCoordinateScope : IDisposable
		{
			// Token: 0x060003EA RID: 1002 RVA: 0x0000F15D File Offset: 0x0000D35D
			public ExitCoordinateScope(CoordinateTrackingVisitor pathScope)
			{
				this.pathScope = pathScope;
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x0000F166 File Offset: 0x0000D366
			public void Dispose()
			{
				if (this.pathScope != null)
				{
					this.pathScope.coordinatePath.Pop();
					this.pathScope = null;
				}
			}

			// Token: 0x040001E7 RID: 487
			private CoordinateTrackingVisitor pathScope;
		}
	}
}
