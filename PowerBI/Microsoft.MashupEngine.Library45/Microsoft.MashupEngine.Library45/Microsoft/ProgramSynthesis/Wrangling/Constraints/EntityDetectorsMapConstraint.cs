using System;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200022E RID: 558
	public class EntityDetectorsMapConstraint<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<DSLOptions>
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002458B File Offset: 0x0002278B
		public EntityDetectorsMapConstraint(EntityDetectorsMap map)
		{
			this._map = map;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002459C File Offset: 0x0002279C
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			EntityDetectorsMapConstraint<TInput, TOutput> entityDetectorsMapConstraint = other as EntityDetectorsMapConstraint<TInput, TOutput>;
			return entityDetectorsMapConstraint != null && object.Equals(this._map, entityDetectorsMapConstraint._map);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000245C8 File Offset: 0x000227C8
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			EntityDetectorsMapConstraint<TInput, TOutput> entityDetectorsMapConstraint = other as EntityDetectorsMapConstraint<TInput, TOutput>;
			return entityDetectorsMapConstraint != null && !object.Equals(this._map, entityDetectorsMapConstraint._map);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000245F5 File Offset: 0x000227F5
		public void SetOptions(DSLOptions options)
		{
			options.EntityDetectorsMap = this._map;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00024603 File Offset: 0x00022803
		public override int GetHashCode()
		{
			return 398341 ^ this._map.GetHashCode();
		}

		// Token: 0x04000616 RID: 1558
		private readonly EntityDetectorsMap _map;
	}
}
