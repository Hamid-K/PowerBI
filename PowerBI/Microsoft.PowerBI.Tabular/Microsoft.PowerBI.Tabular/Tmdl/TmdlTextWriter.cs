using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200014F RID: 335
	internal sealed class TmdlTextWriter : Disposable, ITmdlWriter
	{
		// Token: 0x0600156F RID: 5487 RVA: 0x000901C0 File Offset: 0x0008E3C0
		public TmdlTextWriter(Stream file, TmdlWriterSettings settings = default(TmdlWriterSettings))
		{
			this.writer = new StreamWriter(file, MetadataFormattingOptions.GetEffectiveEncoding(), 1024, true);
			this.settings = (settings.IsValid ? settings : TmdlWriterSettings.Create());
			this.scope = new TmdlTextWriter.WriterScope(this, this.settings.EOL, this.settings.BaseIndentation);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00090224 File Offset: 0x0008E424
		public TmdlTextWriter(TextWriter writer, TmdlWriterSettings settings = default(TmdlWriterSettings))
		{
			this.writer = writer;
			this.settings = (settings.IsValid ? settings : TmdlWriterSettings.Create());
			this.scope = new TmdlTextWriter.WriterScope(this, this.settings.EOL, this.settings.BaseIndentation);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00090278 File Offset: 0x0008E478
		public void Write(params TmdlObject[] tmdlObjects)
		{
			if (tmdlObjects != null && tmdlObjects.Length != 0)
			{
				for (int i = 0; i < tmdlObjects.Length; i++)
				{
					tmdlObjects[i].WriteTo(this);
				}
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000902A8 File Offset: 0x0008E4A8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.writer.Flush();
					this.writer.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000902E8 File Offset: 0x0008E4E8
		IDisposable ITmdlWriter.Indent(int count)
		{
			return this.scope.Indent(count);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x000902F8 File Offset: 0x0008E4F8
		string ITmdlWriter.FormatKeyword(string keyword)
		{
			if (string.IsNullOrEmpty(keyword))
			{
				return keyword;
			}
			switch (this.settings.KeywordStyle)
			{
			case TmdlCasingStyle.CamelCase:
				return keyword.ToJsonCase();
			case TmdlCasingStyle.Pascalcase:
				return keyword.ToCSharpCase();
			case TmdlCasingStyle.LowerCase:
				return keyword.ToLowerInvariant();
			default:
				return keyword;
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00090347 File Offset: 0x0008E547
		void ITmdlWriter.Write(object value)
		{
			this.scope.Write(value);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00090355 File Offset: 0x0008E555
		void ITmdlWriter.Write(string format, params object[] args)
		{
			this.scope.Write(format, args);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00090364 File Offset: 0x0008E564
		void ITmdlWriter.WriteLine(object value)
		{
			this.scope.WriteLine(value);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00090372 File Offset: 0x0008E572
		void ITmdlWriter.WriteLine(string format, params object[] args)
		{
			this.scope.WriteLine(format, args);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00090381 File Offset: 0x0008E581
		void ITmdlWriter.WriteLine()
		{
			this.scope.WriteLine();
		}

		// Token: 0x040003C1 RID: 961
		private readonly TextWriter writer;

		// Token: 0x040003C2 RID: 962
		private readonly TmdlWriterSettings settings;

		// Token: 0x040003C3 RID: 963
		private TmdlTextWriter.WriterScope scope;

		// Token: 0x040003C4 RID: 964
		private Stack<TmdlTextWriter.WriterScope> scopes;

		// Token: 0x02000330 RID: 816
		private class WriterScope : Disposable
		{
			// Token: 0x06002531 RID: 9521 RVA: 0x000E7934 File Offset: 0x000E5B34
			public WriterScope(TmdlTextWriter writer, string eol, Indentation indentation)
				: this(writer, eol, indentation, true)
			{
			}

			// Token: 0x06002532 RID: 9522 RVA: 0x000E7940 File Offset: 0x000E5B40
			private WriterScope(TmdlTextWriter writer, string eol, Indentation indentation, bool isBaseScope)
			{
				this.writer = writer;
				this.eol = eol;
				this.indentation = indentation;
				this.isBaseScope = isBaseScope;
				if (!isBaseScope)
				{
					if (this.writer.scopes == null)
					{
						this.writer.scopes = new Stack<TmdlTextWriter.WriterScope>();
					}
					this.writer.scopes.Push(this.writer.scope);
					this.writer.scope = this;
				}
			}

			// Token: 0x06002533 RID: 9523 RVA: 0x000E79B8 File Offset: 0x000E5BB8
			public TmdlTextWriter.WriterScope Indent(int count)
			{
				Utils.Verify(!this.isLineIndented, "Indentation changes in the middle of a line are not supported!");
				return new TmdlTextWriter.WriterScope(this.writer, this.eol, (count == -1) ? Indentation.Empty : this.indentation.Increment(this.writer.settings.Indentation, count), false);
			}

			// Token: 0x06002534 RID: 9524 RVA: 0x000E7A14 File Offset: 0x000E5C14
			public void Write(object value)
			{
				if (!this.isLineIndented)
				{
					this.writer.writer.Write(this.indentation);
					this.isLineIndented = true;
				}
				this.writer.writer.Write(value);
			}

			// Token: 0x06002535 RID: 9525 RVA: 0x000E7A54 File Offset: 0x000E5C54
			public void Write(string format, object[] args)
			{
				if (!this.isLineIndented)
				{
					this.writer.writer.Write(this.indentation);
					this.isLineIndented = true;
				}
				if (args != null && args.Length != 0)
				{
					this.writer.writer.Write(format, args);
					return;
				}
				this.writer.writer.Write(format);
			}

			// Token: 0x06002536 RID: 9526 RVA: 0x000E7AB6 File Offset: 0x000E5CB6
			public void WriteLine(object value)
			{
				this.Write(value);
				this.WriteLine();
			}

			// Token: 0x06002537 RID: 9527 RVA: 0x000E7AC5 File Offset: 0x000E5CC5
			public void WriteLine(string format, object[] args)
			{
				this.Write(format, args);
				this.WriteLine();
			}

			// Token: 0x06002538 RID: 9528 RVA: 0x000E7AD5 File Offset: 0x000E5CD5
			public void WriteLine()
			{
				this.writer.writer.Write(this.eol);
				this.isLineIndented = false;
			}

			// Token: 0x06002539 RID: 9529 RVA: 0x000E7AF4 File Offset: 0x000E5CF4
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (disposing && !this.isBaseScope)
					{
						this.writer.scope = this.writer.scopes.Pop();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x04000DF3 RID: 3571
			private readonly TmdlTextWriter writer;

			// Token: 0x04000DF4 RID: 3572
			private readonly string eol;

			// Token: 0x04000DF5 RID: 3573
			private readonly Indentation indentation;

			// Token: 0x04000DF6 RID: 3574
			private readonly bool isBaseScope;

			// Token: 0x04000DF7 RID: 3575
			private bool isLineIndented;
		}
	}
}
