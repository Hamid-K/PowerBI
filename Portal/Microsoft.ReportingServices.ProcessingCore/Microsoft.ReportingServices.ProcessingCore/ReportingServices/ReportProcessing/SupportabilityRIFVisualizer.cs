using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200065D RID: 1629
	internal class SupportabilityRIFVisualizer
	{
		// Token: 0x06005A84 RID: 23172 RVA: 0x00173984 File Offset: 0x00171B84
		internal static void DumpTablixes(Report report)
		{
			StreamWriter streamWriter = new StreamWriter(new FileStream("TablixDump.html", FileMode.Create));
			streamWriter.WriteLine("<html><body>");
			for (int i = 0; i < report.ReportSections.Count; i++)
			{
				ReportSection reportSection = report.ReportSections[i];
				for (int j = 0; j < reportSection.ReportItems.Count; j++)
				{
					if (reportSection.ReportItems[j].ObjectType == ObjectType.Tablix)
					{
						SupportabilityRIFVisualizer.DumpTablix((Tablix)reportSection.ReportItems[j], streamWriter);
					}
				}
			}
			streamWriter.WriteLine("</body></html>");
			streamWriter.Flush();
			streamWriter.Close();
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x00173A2C File Offset: 0x00171C2C
		private static void DumpTablix(Tablix tablix, StreamWriter stream)
		{
			stream.Write("Name: ");
			stream.WriteLine(tablix.Name);
			stream.WriteLine("<BR>");
			stream.Write("Width: ");
			stream.WriteLine(tablix.Width);
			stream.WriteLine("<BR>");
			stream.Write("Height: ");
			stream.WriteLine(tablix.Height);
			stream.WriteLine("<BR>");
			if (tablix.InScopeTextBoxes != null)
			{
				stream.WriteLine("<font color=\"darkgreen\"><b>TextBoxesInScope:</b></font> <BR>");
				foreach (TextBox textBox in tablix.InScopeTextBoxes)
				{
					stream.WriteLine("<font color=\"darkgreen\"><b>" + textBox.Name + "</b></font> <BR>");
				}
			}
			stream.Write("<div style='border:solid darkblue 1px;width:");
			stream.Write(tablix.Width);
			stream.Write(";height:");
			stream.Write(tablix.Height);
			stream.WriteLine(";'>");
			stream.WriteLine("<Table cellpadding='0' cellspacing='0' rules='all' border='1'>");
			if (tablix.Corner != null)
			{
				stream.Write("<tr><td colspan='");
				stream.Write(tablix.RowHeaderColumnCount.ToString(CultureInfo.InvariantCulture));
				stream.Write("' rowspan='");
				stream.Write(tablix.ColumnHeaderRowCount.ToString(CultureInfo.InvariantCulture));
				stream.Write("'>Corner</td>");
			}
			Queue<TablixMember> queue = new Queue<TablixMember>();
			if (tablix.TablixColumnMembers != null)
			{
				foreach (object obj in tablix.TablixColumnMembers)
				{
					TablixMember tablixMember = (TablixMember)obj;
					queue.Enqueue(tablixMember);
				}
				SupportabilityRIFVisualizer.DumpTablixMembers(tablix.TablixColumns, queue, stream, 0, 0);
			}
			Global.Tracer.Assert(queue.Count == 0, "(members.Count == 0)");
			int num = 0;
			if (tablix.TablixRowMembers != null)
			{
				foreach (object obj2 in tablix.TablixRowMembers)
				{
					TablixMember tablixMember2 = (TablixMember)obj2;
					SupportabilityRIFVisualizer.DumpTablixMembers(tablix.TablixRows, tablixMember2, stream, -1, ref num);
				}
			}
			stream.WriteLine("</table>");
			stream.WriteLine("</div>");
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x00173CB0 File Offset: 0x00171EB0
		private static void DumpTablixMembers(List<TablixColumn> tablixColumns, Queue<TablixMember> members, StreamWriter stream, int lastLevel, int index)
		{
			if (members.Count == 0)
			{
				stream.Write("</tr>");
				return;
			}
			TablixMember tablixMember = members.Dequeue();
			if (tablixMember.HeaderLevel > lastLevel)
			{
				stream.Write("</tr><tr>");
				lastLevel = tablixMember.HeaderLevel;
			}
			if (tablixMember.ID > -1)
			{
				SupportabilityRIFVisualizer.DumpTablixMember(tablixMember, stream);
			}
			if (tablixMember.SubMembers != null)
			{
				if (tablixMember.RowSpan > 1)
				{
					if (tablixMember.ID > -1)
					{
						TablixMember tablixMember2 = new TablixMember();
						tablixMember2.SubMembers = new TablixMemberList();
						tablixMember2.RowSpan = tablixMember.RowSpan - 1;
						tablixMember2.ID = -1;
						tablixMember2.HeaderLevel = tablixMember.HeaderLevel + 1;
						members.Enqueue(tablixMember2);
						using (IEnumerator enumerator = tablixMember.SubMembers.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								TablixMember tablixMember3 = (TablixMember)obj;
								tablixMember2.SubMembers.Add(tablixMember3);
							}
							goto IL_0142;
						}
					}
					tablixMember.RowSpan--;
					members.Enqueue(tablixMember);
				}
				else
				{
					foreach (object obj2 in tablixMember.SubMembers)
					{
						TablixMember tablixMember4 = (TablixMember)obj2;
						members.Enqueue(tablixMember4);
					}
				}
			}
			IL_0142:
			SupportabilityRIFVisualizer.DumpTablixMembers(tablixColumns, members, stream, lastLevel, index);
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x00173E28 File Offset: 0x00172028
		private static void DumpTablixMembers(TablixRowList tablixRows, TablixMember member, StreamWriter stream, int lastLevel, ref int index)
		{
			if (index > lastLevel)
			{
				stream.Write("<tr style='border:solid darkred 1px;height:" + tablixRows[index].Height + ";'>");
				lastLevel = index;
			}
			if (member.ID > -1)
			{
				SupportabilityRIFVisualizer.DumpTablixMember(member, stream);
			}
			if (member.SubMembers != null)
			{
				if (member.ColSpan > 1)
				{
					if (member.ID > -1)
					{
						TablixMember tablixMember = new TablixMember();
						tablixMember.SubMembers = new TablixMemberList();
						tablixMember.ColSpan = member.ColSpan - 1;
						tablixMember.ID = -1;
						tablixMember.HeaderLevel = member.HeaderLevel + 1;
						foreach (object obj in member.SubMembers)
						{
							TablixMember tablixMember2 = (TablixMember)obj;
							tablixMember.SubMembers.Add(tablixMember2);
						}
						member = tablixMember;
					}
					else
					{
						member.ColSpan--;
					}
					SupportabilityRIFVisualizer.DumpTablixMembers(tablixRows, member, stream, lastLevel, ref index);
					return;
				}
				using (IEnumerator enumerator = member.SubMembers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						TablixMember tablixMember3 = (TablixMember)obj2;
						SupportabilityRIFVisualizer.DumpTablixMembers(tablixRows, tablixMember3, stream, lastLevel, ref index);
					}
					return;
				}
			}
			foreach (object obj3 in tablixRows[index].TablixCells)
			{
				TablixCell tablixCell = (TablixCell)obj3;
				if (tablixCell.ColSpan > 0)
				{
					stream.Write("<td colspan='");
					stream.Write(tablixCell.ColSpan);
					stream.Write("'>");
					if (tablixCell.CellContents != null)
					{
						stream.Write("<div style='overflow:auto;border:solid blue 1px;width:");
						stream.Write(tablixCell.CellContents.Width);
						stream.Write(";height:");
						stream.Write(tablixCell.CellContents.Height);
						stream.WriteLine(";'>");
						stream.Write("CellContents Type: ");
						stream.WriteLine(tablixCell.CellContents.ObjectType.ToString());
						stream.Write("<BR>Name: <font color=\"darkgreen\"><b>");
						stream.WriteLine(tablixCell.CellContents.Name);
						stream.Write(" </b></font><BR>Width: ");
						stream.WriteLine(tablixCell.CellContents.Width);
						stream.Write(" <BR>Height: ");
						stream.WriteLine(tablixCell.CellContents.Height);
						if (tablixCell.CellContents.ObjectType == ObjectType.Textbox)
						{
							stream.WriteLine("<b>");
							stream.WriteLine("</b><BR>");
						}
						else if (tablixCell.CellContents.ObjectType == ObjectType.Tablix)
						{
							SupportabilityRIFVisualizer.DumpTablix((Tablix)tablixCell.CellContents, stream);
						}
						stream.Write("</div>");
					}
					stream.Write("</td>");
				}
			}
			index++;
			stream.Write("</tr>");
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x00174174 File Offset: 0x00172374
		private static void DumpTablixMember(TablixMember member, StreamWriter stream)
		{
			if (member.TablixHeader == null)
			{
				return;
			}
			stream.Write("<td ");
			stream.Write("rowspan='");
			stream.Write(member.RowSpan);
			stream.Write("' colSpan='");
			stream.Write(member.ColSpan);
			stream.Write("'>");
			stream.Write("<div style='overflow:auto;border:solid darkgreen 1px;");
			if (member.TablixHeader.CellContents != null)
			{
				stream.Write("height:");
				stream.Write(member.TablixHeader.CellContents.Height);
				stream.Write(";");
				stream.Write("width:");
				stream.Write(member.TablixHeader.CellContents.Width);
				stream.Write(";");
			}
			stream.WriteLine("'>");
			stream.WriteLine("MemberCellIndex = " + member.MemberCellIndex.ToString());
			if (member.Grouping != null)
			{
				stream.WriteLine("Dynamic<BR>");
				stream.Write("Grouping ");
				stream.Write("Name: <b><font color=\"darkblue\">");
				stream.WriteLine(member.Grouping.Name);
				stream.WriteLine("</font></b><BR>");
				if (member.Grouping.Variables != null)
				{
					foreach (Variable variable in member.Grouping.Variables)
					{
						stream.WriteLine(string.Concat(new string[]
						{
							"<font color=\"darkred\"><b>",
							variable.Name,
							"</b></font> ",
							variable.Value.OriginalText,
							"<BR>"
						}));
					}
				}
				if (member.InScopeTextBoxes == null)
				{
					goto IL_0219;
				}
				stream.WriteLine("<font color=\"darkgreen\"><b>TextBoxesInScope:</b></font> <BR>");
				using (List<TextBox>.Enumerator enumerator2 = member.InScopeTextBoxes.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						TextBox textBox = enumerator2.Current;
						stream.WriteLine("<font color=\"darkgreen\"><b>" + textBox.Name + "</b></font> <BR>");
					}
					goto IL_0219;
				}
			}
			stream.WriteLine("Static<BR>");
			IL_0219:
			stream.WriteLine("RowSpan: " + member.RowSpan.ToString() + "<BR>");
			stream.WriteLine("ColSpan: " + member.ColSpan.ToString() + "<BR>");
			if (member.HasConditionalOrToggleableVisibility)
			{
				stream.WriteLine("HasConditionalOrToggleableVisibility<BR>");
			}
			if (member.IsAutoSubtotal)
			{
				stream.WriteLine("IsAutoSubtotal<BR>");
			}
			if (member.IsInnerMostMemberWithHeader)
			{
				stream.WriteLine("IsInnerMostMemberWithHeader<BR>");
			}
			stream.Write("HeaderSize: ");
			stream.WriteLine(member.TablixHeader.Size);
			stream.WriteLine("<BR>");
			if (member.TablixHeader.CellContents != null)
			{
				stream.Write("CellContents Type: ");
				stream.WriteLine(member.TablixHeader.CellContents.ObjectType.ToString());
				stream.WriteLine("<BR>");
				stream.Write(" \tName: <b><font color=\"darkgreen\">");
				stream.WriteLine(member.TablixHeader.CellContents.Name);
				stream.WriteLine("</font></b><BR>");
				stream.Write(" \tWidth: ");
				stream.WriteLine(member.TablixHeader.CellContents.Width);
				stream.WriteLine("<BR>");
				stream.Write(" \tHeight: ");
				stream.WriteLine(member.TablixHeader.CellContents.Height);
				stream.WriteLine("<BR>");
				if (member.TablixHeader.CellContents.ObjectType == ObjectType.Textbox)
				{
					TextBox textBox2 = (TextBox)member.TablixHeader.CellContents;
					stream.Write("<b>");
					stream.Write("</b>");
					if (textBox2.UserSort != null)
					{
						stream.WriteLine("sort expr scope: " + textBox2.UserSort.SortExpressionScopeString);
						stream.WriteLine("<BR>");
						stream.WriteLine("sort target: " + textBox2.UserSort.SortTargetString);
						stream.WriteLine("<BR>");
					}
					stream.WriteLine("<BR>");
				}
				else if (member.TablixHeader.CellContents.ObjectType == ObjectType.Tablix)
				{
					SupportabilityRIFVisualizer.DumpTablix((Tablix)member.TablixHeader.CellContents, stream);
				}
			}
			stream.Write("</div>");
			stream.Write("</td>");
		}
	}
}
