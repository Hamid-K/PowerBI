using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000966 RID: 2406
	internal abstract class MdxCube : ICube
	{
		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x000E6694 File Offset: 0x000E4894
		public MdxMeasure CountMeasure
		{
			get
			{
				if (this.countMeasure == null)
				{
					this.countMeasure = new MdxMeasure(this, MeasureValue.Count.Measure.Identifier, MeasureValue.Count.Measure.Identifier, OleDbType.BigInt, null, null, false, null);
				}
				return this.countMeasure;
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x000E66E0 File Offset: 0x000E48E0
		public MdxMeasure MeasureOfOneMeasure
		{
			get
			{
				if (this.measureOfOneMeasure == null)
				{
					string text = this.MeasuresDimensionName + ".[Microsoft.Mashup.Engine.One]";
					this.measureOfOneMeasure = new MdxMeasure(this, text, text, OleDbType.BigInt, null, null, false, null);
				}
				return this.measureOfOneMeasure;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06004484 RID: 17540
		public abstract string MdxIdentifier { get; }

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06004485 RID: 17541
		public abstract IDictionary<string, MdxDimension> Dimensions { get; }

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06004486 RID: 17542
		public abstract IList<MdxMeasure> Measures { get; }

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06004487 RID: 17543
		public abstract IList<MdxKpi> Kpis { get; }

		// Token: 0x06004488 RID: 17544
		public abstract MdxMeasureGroup GetMeasureGroup(string measureGroupName);

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06004489 RID: 17545 RVA: 0x000E6720 File Offset: 0x000E4920
		public virtual string MeasuresDimensionName
		{
			get
			{
				return "[Measures]";
			}
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x000E6728 File Offset: 0x000E4928
		public virtual bool TryGetLevelFromIdentifier(string identifier, out string levelUniqueName)
		{
			MdxIdentifier mdxIdentifier;
			if (Microsoft.Mashup.Engine1.Library.Mdx.MdxIdentifier.TryParse(identifier, out mdxIdentifier) && mdxIdentifier.LevelUniqueName != null)
			{
				levelUniqueName = mdxIdentifier.LevelUniqueName;
				return true;
			}
			levelUniqueName = null;
			return false;
		}

		// Token: 0x0600448B RID: 17547
		public abstract MdxExpression CompileLevelMemberUserDefined(IdentifierCubeExpression identifier, MdxExpression mdx, MdxProperty property);

		// Token: 0x0600448C RID: 17548 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetDefaultMeasure(out string identifier)
		{
			identifier = null;
			return false;
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x000E675C File Offset: 0x000E495C
		public bool TryGetObject(string identifier, out MdxCubeObject obj)
		{
			if (identifier == this.MeasureOfOneMeasure.MdxIdentifier)
			{
				obj = this.MeasureOfOneMeasure;
				return true;
			}
			if (this.TryGetObjectFromMdxIdentifier(identifier, out obj))
			{
				return true;
			}
			if (identifier == MeasureValue.Count.Measure.Identifier)
			{
				obj = this.CountMeasure;
				return true;
			}
			return this.LazyLoadPropertiesFromIdentifier(identifier) && this.TryGetObjectFromMdxIdentifier(identifier, out obj);
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x000E67C8 File Offset: 0x000E49C8
		public MdxCubeObject GetObject(string identifier)
		{
			MdxCubeObject mdxCubeObject;
			if (!this.TryGetObject(identifier, out mdxCubeObject))
			{
				throw new IndexOutOfRangeException(identifier);
			}
			return mdxCubeObject;
		}

		// Token: 0x0600448F RID: 17551
		protected abstract bool TryGetObjectFromMdxIdentifier(string identifier, out MdxCubeObject obj);

		// Token: 0x06004490 RID: 17552 RVA: 0x000E67E8 File Offset: 0x000E49E8
		private bool LazyLoadPropertiesFromIdentifier(string identifier)
		{
			string text;
			MdxCubeObject mdxCubeObject;
			return this.TryGetLevelFromIdentifier(identifier, out text) && this.TryGetObjectFromMdxIdentifier(text, out mdxCubeObject) && ((MdxLevel)mdxCubeObject).Properties != null;
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06004491 RID: 17553 RVA: 0x000E681B File Offset: 0x000E4A1B
		IdentifierCubeExpression ICube.Identifier
		{
			get
			{
				return new IdentifierCubeExpression(this.MdxIdentifier);
			}
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x000E6828 File Offset: 0x000E4A28
		bool ICube.TryGetObject(IdentifierCubeExpression identifier, out ICubeObject obj)
		{
			MdxCubeObject mdxCubeObject;
			if (this.TryGetObject(identifier.Identifier, out mdxCubeObject))
			{
				obj = mdxCubeObject;
				return true;
			}
			obj = null;
			return false;
		}

		// Token: 0x04002480 RID: 9344
		public const string MashupEnginePrefix = "Microsoft.Mashup.Engine";

		// Token: 0x04002481 RID: 9345
		private MdxMeasure countMeasure;

		// Token: 0x04002482 RID: 9346
		private MdxMeasure measureOfOneMeasure;
	}
}
