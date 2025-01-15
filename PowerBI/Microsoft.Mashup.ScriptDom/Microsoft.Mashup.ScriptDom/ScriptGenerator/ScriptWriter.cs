using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x02000191 RID: 401
	internal class ScriptWriter
	{
		// Token: 0x0600216A RID: 8554 RVA: 0x0015DE04 File Offset: 0x0015C004
		public ScriptWriter(SqlScriptGeneratorOptions options)
		{
			this._options = options;
			this._alignmentPointDataMap = new Dictionary<AlignmentPoint, ScriptWriter.AlignmentPointData>();
			this._alignmentPointNameMapForCurrentScope = new Dictionary<string, AlignmentPoint>();
			this._alignmentPointNameMaps = new Stack<Dictionary<string, AlignmentPoint>>();
			this._scriptWriterElements = new List<ScriptWriter.ScriptWriterElement>();
			this._newLineAlignmentPoints = new Stack<AlignmentPoint>();
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0015DE58 File Offset: 0x0015C058
		public void AddKeyword(TSqlTokenType keywordId)
		{
			string tokenString = ScriptGeneratorSupporter.GetTokenString(keywordId, this._options.KeywordCasing);
			TSqlParserToken tsqlParserToken = new TSqlParserToken(keywordId, tokenString);
			this.AddToken(tsqlParserToken);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0015DE86 File Offset: 0x0015C086
		public void AddIdentifierWithCasing(string text)
		{
			ScriptGeneratorSupporter.CheckForNullReference(text, "text");
			this.AddIdentifier(text, true);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0015DE9B File Offset: 0x0015C09B
		public void AddIdentifierWithoutCasing(string text)
		{
			ScriptGeneratorSupporter.CheckForNullReference(text, "text");
			this.AddIdentifier(text, false);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0015DEB0 File Offset: 0x0015C0B0
		public void AddToken(TSqlParserToken token)
		{
			ScriptGeneratorSupporter.CheckForNullReference(token, "token");
			this.AddTokenWrapper(new ScriptWriter.TokenWrapper(token));
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0015DEC9 File Offset: 0x0015C0C9
		public void NewLine()
		{
			this.AddNewLine();
			if (this._newLineAlignmentPoints.Count > 0)
			{
				this.Mark(this._newLineAlignmentPoints.Peek());
			}
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0015DEF0 File Offset: 0x0015C0F0
		public void Indent(int size)
		{
			this.AddSpace(size);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0015DEF9 File Offset: 0x0015C0F9
		public void Mark(AlignmentPoint ap)
		{
			if (!string.IsNullOrEmpty(ap.Name) && !this._alignmentPointNameMapForCurrentScope.ContainsKey(ap.Name))
			{
				this._alignmentPointNameMapForCurrentScope.Add(ap.Name, ap);
			}
			this.AddAlignmentPoint(ap);
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0015DF34 File Offset: 0x0015C134
		public void PushNewLineAlignmentPoint(AlignmentPoint ap)
		{
			this._newLineAlignmentPoints.Push(ap);
			this._alignmentPointNameMaps.Push(this._alignmentPointNameMapForCurrentScope);
			this._alignmentPointNameMapForCurrentScope = new Dictionary<string, AlignmentPoint>();
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0015DF5E File Offset: 0x0015C15E
		public void PopNewLineAlignmentPoint()
		{
			this._newLineAlignmentPoints.Pop();
			this._alignmentPointNameMapForCurrentScope = this._alignmentPointNameMaps.Pop();
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0015DF80 File Offset: 0x0015C180
		public AlignmentPoint FindOrCreateAlignmentPoint(string name)
		{
			AlignmentPoint alignmentPoint = null;
			if (!this._alignmentPointNameMapForCurrentScope.TryGetValue(name, ref alignmentPoint))
			{
				alignmentPoint = null;
			}
			if (alignmentPoint == null)
			{
				alignmentPoint = new AlignmentPoint(name);
			}
			return alignmentPoint;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0015DFAC File Offset: 0x0015C1AC
		public void WriteTo(TextWriter writer)
		{
			List<TSqlParserToken> list = this.TryGetAlignedTokens();
			foreach (TSqlParserToken tsqlParserToken in list)
			{
				writer.Write(tsqlParserToken.Text);
			}
			writer.Flush();
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0015E00C File Offset: 0x0015C20C
		public void WriteTo(IList<TSqlParserToken> tokens)
		{
			List<TSqlParserToken> list = this.TryGetAlignedTokens();
			foreach (TSqlParserToken tsqlParserToken in list)
			{
				tokens.Add(tsqlParserToken);
			}
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0015E064 File Offset: 0x0015C264
		private void AddIdentifier(string text, bool applyCasing)
		{
			if (applyCasing)
			{
				text = ScriptGeneratorSupporter.GetCasedString(text, this._options.KeywordCasing);
			}
			TSqlParserToken tsqlParserToken = new TSqlParserToken(TSqlTokenType.Identifier, text);
			this.AddToken(tsqlParserToken);
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0015E09A File Offset: 0x0015C29A
		private void AddSpace(int count)
		{
			this.AddToken(ScriptGeneratorSupporter.CreateWhitespaceToken(count));
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0015E0A8 File Offset: 0x0015C2A8
		private void AddTokenWrapper(ScriptWriter.TokenWrapper token)
		{
			this._scriptWriterElements.Add(token);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0015E0B6 File Offset: 0x0015C2B6
		private void AddAlignmentPoint(AlignmentPoint ap)
		{
			this._scriptWriterElements.Add(this.FindOrCreateAlignmentPointData(ap));
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0015E0CA File Offset: 0x0015C2CA
		private void AddNewLine()
		{
			this._scriptWriterElements.Add(ScriptWriter._newLine);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0015E0DC File Offset: 0x0015C2DC
		private ScriptWriter.ScriptWriterElement FindOrCreateAlignmentPointData(AlignmentPoint ap)
		{
			ScriptWriter.AlignmentPointData alignmentPointData;
			if (!this._alignmentPointDataMap.TryGetValue(ap, ref alignmentPointData))
			{
				alignmentPointData = new ScriptWriter.AlignmentPointData(ap.Name);
				this._alignmentPointDataMap.Add(ap, alignmentPointData);
			}
			return alignmentPointData;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0015E114 File Offset: 0x0015C314
		private List<TSqlParserToken> TryGetAlignedTokens()
		{
			List<TSqlParserToken> list = this.Align();
			if (list == null)
			{
				list = this.GetAllTokens();
			}
			return list;
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0015E134 File Offset: 0x0015C334
		private List<TSqlParserToken> Align()
		{
			HashSet<ScriptWriter.AlignmentPointData> hashSet = new HashSet<ScriptWriter.AlignmentPointData>();
			int num = 0;
			ScriptWriter.AlignmentPointData alignmentPointData = null;
			for (int i = 0; i < this._scriptWriterElements.Count; i++)
			{
				ScriptWriter.ScriptWriterElement scriptWriterElement = this._scriptWriterElements[i];
				switch (scriptWriterElement.ElementType)
				{
				case ScriptWriter.ScriptWriterElementType.AlignmentPoint:
				{
					ScriptWriter.AlignmentPointData alignmentPointData2 = scriptWriterElement as ScriptWriter.AlignmentPointData;
					hashSet.Add(alignmentPointData2);
					if (alignmentPointData != null)
					{
						alignmentPointData2.AddLeftPoint(alignmentPointData, num);
						alignmentPointData.AddRightPoint(alignmentPointData2);
					}
					else
					{
						alignmentPointData2.Offset = Math.Max(alignmentPointData2.Offset, num);
					}
					num = 0;
					alignmentPointData = alignmentPointData2;
					break;
				}
				case ScriptWriter.ScriptWriterElementType.Token:
				{
					ScriptWriter.TokenWrapper tokenWrapper = scriptWriterElement as ScriptWriter.TokenWrapper;
					if (tokenWrapper != null && tokenWrapper.Token != null && tokenWrapper.Token.Text != null)
					{
						num += tokenWrapper.Token.Text.Length;
					}
					break;
				}
				case ScriptWriter.ScriptWriterElementType.NewLine:
					num = 0;
					alignmentPointData = null;
					break;
				}
			}
			while (hashSet.Count != 0)
			{
				ScriptWriter.AlignmentPointData alignmentPointData3 = ScriptWriter.FindOneAlignmentPointWithOutDependent(hashSet);
				if (alignmentPointData3 == null)
				{
					return null;
				}
				HashSet<ScriptWriter.AlignmentPointData> rightPoints = alignmentPointData3.RightPoints;
				foreach (ScriptWriter.AlignmentPointData alignmentPointData4 in rightPoints)
				{
					alignmentPointData4.AlignAndRemoveLeftPoint(alignmentPointData3);
				}
				hashSet.Remove(alignmentPointData3);
			}
			List<TSqlParserToken> list = new List<TSqlParserToken>();
			int num2 = 0;
			for (int j = 0; j < this._scriptWriterElements.Count; j++)
			{
				ScriptWriter.ScriptWriterElement scriptWriterElement2 = this._scriptWriterElements[j];
				switch (scriptWriterElement2.ElementType)
				{
				case ScriptWriter.ScriptWriterElementType.AlignmentPoint:
				{
					ScriptWriter.AlignmentPointData alignmentPointData5 = scriptWriterElement2 as ScriptWriter.AlignmentPointData;
					if (alignmentPointData5.Offset > num2)
					{
						list.Add(ScriptGeneratorSupporter.CreateWhitespaceToken(alignmentPointData5.Offset - num2));
					}
					num2 = alignmentPointData5.Offset;
					break;
				}
				case ScriptWriter.ScriptWriterElementType.Token:
				{
					ScriptWriter.TokenWrapper tokenWrapper2 = scriptWriterElement2 as ScriptWriter.TokenWrapper;
					if (tokenWrapper2 != null && tokenWrapper2.Token != null && tokenWrapper2.Token.Text != null)
					{
						list.Add(tokenWrapper2.Token);
						num2 += tokenWrapper2.Token.Text.Length;
					}
					break;
				}
				case ScriptWriter.ScriptWriterElementType.NewLine:
					list.Add(ScriptWriter._newLineToken);
					num2 = 0;
					break;
				}
			}
			return list;
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0015E374 File Offset: 0x0015C574
		private List<TSqlParserToken> GetAllTokens()
		{
			List<TSqlParserToken> list = new List<TSqlParserToken>();
			for (int i = 0; i < this._scriptWriterElements.Count; i++)
			{
				ScriptWriter.ScriptWriterElement scriptWriterElement = this._scriptWriterElements[i];
				switch (scriptWriterElement.ElementType)
				{
				case ScriptWriter.ScriptWriterElementType.Token:
				{
					ScriptWriter.TokenWrapper tokenWrapper = scriptWriterElement as ScriptWriter.TokenWrapper;
					list.Add(tokenWrapper.Token);
					break;
				}
				case ScriptWriter.ScriptWriterElementType.NewLine:
					list.Add(ScriptWriter._newLineToken);
					break;
				}
			}
			return list;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0015E3E8 File Offset: 0x0015C5E8
		private static ScriptWriter.AlignmentPointData FindOneAlignmentPointWithOutDependent(HashSet<ScriptWriter.AlignmentPointData> points)
		{
			ScriptWriter.AlignmentPointData alignmentPointData = null;
			foreach (ScriptWriter.AlignmentPointData alignmentPointData2 in points)
			{
				if (alignmentPointData2.HasNoLeftPoints)
				{
					alignmentPointData = alignmentPointData2;
					break;
				}
			}
			return alignmentPointData;
		}

		// Token: 0x040019A6 RID: 6566
		private static ScriptWriter.NewLineElement _newLine = new ScriptWriter.NewLineElement();

		// Token: 0x040019A7 RID: 6567
		private static TSqlParserToken _newLineToken = new TSqlParserToken(TSqlTokenType.WhiteSpace, Environment.NewLine);

		// Token: 0x040019A8 RID: 6568
		private SqlScriptGeneratorOptions _options;

		// Token: 0x040019A9 RID: 6569
		private Dictionary<AlignmentPoint, ScriptWriter.AlignmentPointData> _alignmentPointDataMap;

		// Token: 0x040019AA RID: 6570
		private Dictionary<string, AlignmentPoint> _alignmentPointNameMapForCurrentScope;

		// Token: 0x040019AB RID: 6571
		private Stack<Dictionary<string, AlignmentPoint>> _alignmentPointNameMaps;

		// Token: 0x040019AC RID: 6572
		private List<ScriptWriter.ScriptWriterElement> _scriptWriterElements;

		// Token: 0x040019AD RID: 6573
		private Stack<AlignmentPoint> _newLineAlignmentPoints;

		// Token: 0x02000192 RID: 402
		internal abstract class ScriptWriterElement
		{
			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06002183 RID: 8579 RVA: 0x0015E469 File Offset: 0x0015C669
			// (set) Token: 0x06002182 RID: 8578 RVA: 0x0015E460 File Offset: 0x0015C660
			public ScriptWriter.ScriptWriterElementType ElementType { get; protected set; }
		}

		// Token: 0x02000193 RID: 403
		internal class AlignmentPointData : ScriptWriter.ScriptWriterElement
		{
			// Token: 0x06002185 RID: 8581 RVA: 0x0015E479 File Offset: 0x0015C679
			public AlignmentPointData(string name)
			{
				base.ElementType = ScriptWriter.ScriptWriterElementType.AlignmentPoint;
				this.Name = name;
				this._leftPoints = new Dictionary<ScriptWriter.AlignmentPointData, int>();
				this._rightPoints = new HashSet<ScriptWriter.AlignmentPointData>();
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06002186 RID: 8582 RVA: 0x0015E4A5 File Offset: 0x0015C6A5
			// (set) Token: 0x06002187 RID: 8583 RVA: 0x0015E4AD File Offset: 0x0015C6AD
			public int Offset { get; set; }

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06002188 RID: 8584 RVA: 0x0015E4B6 File Offset: 0x0015C6B6
			// (set) Token: 0x06002189 RID: 8585 RVA: 0x0015E4BE File Offset: 0x0015C6BE
			public string Name { get; private set; }

			// Token: 0x0600218A RID: 8586 RVA: 0x0015E4C8 File Offset: 0x0015C6C8
			public void AddLeftPoint(ScriptWriter.AlignmentPointData ap, int width)
			{
				int num;
				if (!this._leftPoints.TryGetValue(ap, ref num))
				{
					this._leftPoints.Add(ap, width);
					return;
				}
				if (num < width)
				{
					this._leftPoints[ap] = width;
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600218B RID: 8587 RVA: 0x0015E504 File Offset: 0x0015C704
			public bool HasNoLeftPoints
			{
				get
				{
					return this._leftPoints.Count == 0;
				}
			}

			// Token: 0x0600218C RID: 8588 RVA: 0x0015E514 File Offset: 0x0015C714
			public void AddRightPoint(ScriptWriter.AlignmentPointData ap)
			{
				this._rightPoints.Add(ap);
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x0600218D RID: 8589 RVA: 0x0015E523 File Offset: 0x0015C723
			public HashSet<ScriptWriter.AlignmentPointData> RightPoints
			{
				get
				{
					return this._rightPoints;
				}
			}

			// Token: 0x0600218E RID: 8590 RVA: 0x0015E52C File Offset: 0x0015C72C
			public void AlignAndRemoveLeftPoint(ScriptWriter.AlignmentPointData ap)
			{
				int num;
				if (this._leftPoints.TryGetValue(ap, ref num))
				{
					this.Offset = Math.Max(ap.Offset + num, this.Offset);
					this._leftPoints.Remove(ap);
				}
			}

			// Token: 0x040019AF RID: 6575
			private Dictionary<ScriptWriter.AlignmentPointData, int> _leftPoints;

			// Token: 0x040019B0 RID: 6576
			private HashSet<ScriptWriter.AlignmentPointData> _rightPoints;
		}

		// Token: 0x02000194 RID: 404
		[DebuggerDisplay("Token({_token.Text})")]
		internal class TokenWrapper : ScriptWriter.ScriptWriterElement
		{
			// Token: 0x0600218F RID: 8591 RVA: 0x0015E56F File Offset: 0x0015C76F
			public TokenWrapper(TSqlParserToken token)
			{
				this._token = token;
				base.ElementType = ScriptWriter.ScriptWriterElementType.Token;
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06002190 RID: 8592 RVA: 0x0015E585 File Offset: 0x0015C785
			public TSqlParserToken Token
			{
				get
				{
					return this._token;
				}
			}

			// Token: 0x040019B3 RID: 6579
			private TSqlParserToken _token;
		}

		// Token: 0x02000195 RID: 405
		[DebuggerDisplay("NewLine==========")]
		internal class NewLineElement : ScriptWriter.ScriptWriterElement
		{
			// Token: 0x06002191 RID: 8593 RVA: 0x0015E58D File Offset: 0x0015C78D
			public NewLineElement()
			{
				base.ElementType = ScriptWriter.ScriptWriterElementType.NewLine;
			}
		}

		// Token: 0x02000196 RID: 406
		internal enum ScriptWriterElementType
		{
			// Token: 0x040019B5 RID: 6581
			AlignmentPoint,
			// Token: 0x040019B6 RID: 6582
			Token,
			// Token: 0x040019B7 RID: 6583
			NewLine
		}
	}
}
