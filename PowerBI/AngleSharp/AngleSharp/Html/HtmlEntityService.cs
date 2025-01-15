using System;
using System.Collections.Generic;
using AngleSharp.Extensions;
using AngleSharp.Services;

namespace AngleSharp.Html
{
	// Token: 0x020000B7 RID: 183
	public sealed class HtmlEntityService : IEntityProvider
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x000209B8 File Offset: 0x0001EBB8
		private HtmlEntityService()
		{
			this._entities = new Dictionary<char, Dictionary<string, string>>
			{
				{
					'a',
					this.GetSymbolLittleA()
				},
				{
					'A',
					this.GetSymbolBigA()
				},
				{
					'b',
					this.GetSymbolLittleB()
				},
				{
					'B',
					this.GetSymbolBigB()
				},
				{
					'c',
					this.GetSymbolLittleC()
				},
				{
					'C',
					this.GetSymbolBigC()
				},
				{
					'd',
					this.GetSymbolLittleD()
				},
				{
					'D',
					this.GetSymbolBigD()
				},
				{
					'e',
					this.GetSymbolLittleE()
				},
				{
					'E',
					this.GetSymbolBigE()
				},
				{
					'f',
					this.GetSymbolLittleF()
				},
				{
					'F',
					this.GetSymbolBigF()
				},
				{
					'g',
					this.GetSymbolLittleG()
				},
				{
					'G',
					this.GetSymbolBigG()
				},
				{
					'h',
					this.GetSymbolLittleH()
				},
				{
					'H',
					this.GetSymbolBigH()
				},
				{
					'i',
					this.GetSymbolLittleI()
				},
				{
					'I',
					this.GetSymbolBigI()
				},
				{
					'j',
					this.GetSymbolLittleJ()
				},
				{
					'J',
					this.GetSymbolBigJ()
				},
				{
					'k',
					this.GetSymbolLittleK()
				},
				{
					'K',
					this.GetSymbolBigK()
				},
				{
					'l',
					this.GetSymbolLittleL()
				},
				{
					'L',
					this.GetSymbolBigL()
				},
				{
					'm',
					this.GetSymbolLittleM()
				},
				{
					'M',
					this.GetSymbolBigM()
				},
				{
					'n',
					this.GetSymbolLittleN()
				},
				{
					'N',
					this.GetSymbolBigN()
				},
				{
					'o',
					this.GetSymbolLittleO()
				},
				{
					'O',
					this.GetSymbolBigO()
				},
				{
					'p',
					this.GetSymbolLittleP()
				},
				{
					'P',
					this.GetSymbolBigP()
				},
				{
					'q',
					this.GetSymbolLittleQ()
				},
				{
					'Q',
					this.GetSymbolBigQ()
				},
				{
					'r',
					this.GetSymbolLittleR()
				},
				{
					'R',
					this.GetSymbolBigR()
				},
				{
					's',
					this.GetSymbolLittleS()
				},
				{
					'S',
					this.GetSymbolBigS()
				},
				{
					't',
					this.GetSymbolLittleT()
				},
				{
					'T',
					this.GetSymbolBigT()
				},
				{
					'u',
					this.GetSymbolLittleU()
				},
				{
					'U',
					this.GetSymbolBigU()
				},
				{
					'v',
					this.GetSymbolLittleV()
				},
				{
					'V',
					this.GetSymbolBigV()
				},
				{
					'w',
					this.GetSymbolLittleW()
				},
				{
					'W',
					this.GetSymbolBigW()
				},
				{
					'x',
					this.GetSymbolLittleX()
				},
				{
					'X',
					this.GetSymbolBigX()
				},
				{
					'y',
					this.GetSymbolLittleY()
				},
				{
					'Y',
					this.GetSymbolBigY()
				},
				{
					'z',
					this.GetSymbolLittleZ()
				},
				{
					'Z',
					this.GetSymbolBigZ()
				}
			};
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00020CB0 File Offset: 0x0001EEB0
		private Dictionary<string, string> GetSymbolLittleA()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "aacute;", HtmlEntityService.Convert(225));
			HtmlEntityService.AddSingle(dictionary, "abreve;", HtmlEntityService.Convert(259));
			HtmlEntityService.AddSingle(dictionary, "ac;", HtmlEntityService.Convert(8766));
			HtmlEntityService.AddSingle(dictionary, "acd;", HtmlEntityService.Convert(8767));
			HtmlEntityService.AddSingle(dictionary, "acE;", HtmlEntityService.Convert(8766, 819));
			HtmlEntityService.AddBoth(dictionary, "acirc;", HtmlEntityService.Convert(226));
			HtmlEntityService.AddBoth(dictionary, "acute;", HtmlEntityService.Convert(180));
			HtmlEntityService.AddSingle(dictionary, "acy;", HtmlEntityService.Convert(1072));
			HtmlEntityService.AddBoth(dictionary, "aelig;", HtmlEntityService.Convert(230));
			HtmlEntityService.AddSingle(dictionary, "af;", HtmlEntityService.Convert(8289));
			HtmlEntityService.AddSingle(dictionary, "afr;", HtmlEntityService.Convert(120094));
			HtmlEntityService.AddBoth(dictionary, "agrave;", HtmlEntityService.Convert(224));
			HtmlEntityService.AddSingle(dictionary, "alefsym;", HtmlEntityService.Convert(8501));
			HtmlEntityService.AddSingle(dictionary, "aleph;", HtmlEntityService.Convert(8501));
			HtmlEntityService.AddSingle(dictionary, "alpha;", HtmlEntityService.Convert(945));
			HtmlEntityService.AddSingle(dictionary, "amacr;", HtmlEntityService.Convert(257));
			HtmlEntityService.AddSingle(dictionary, "amalg;", HtmlEntityService.Convert(10815));
			HtmlEntityService.AddBoth(dictionary, "amp;", HtmlEntityService.Convert(38));
			HtmlEntityService.AddSingle(dictionary, "and;", HtmlEntityService.Convert(8743));
			HtmlEntityService.AddSingle(dictionary, "andand;", HtmlEntityService.Convert(10837));
			HtmlEntityService.AddSingle(dictionary, "andd;", HtmlEntityService.Convert(10844));
			HtmlEntityService.AddSingle(dictionary, "andslope;", HtmlEntityService.Convert(10840));
			HtmlEntityService.AddSingle(dictionary, "andv;", HtmlEntityService.Convert(10842));
			HtmlEntityService.AddSingle(dictionary, "ang;", HtmlEntityService.Convert(8736));
			HtmlEntityService.AddSingle(dictionary, "ange;", HtmlEntityService.Convert(10660));
			HtmlEntityService.AddSingle(dictionary, "angle;", HtmlEntityService.Convert(8736));
			HtmlEntityService.AddSingle(dictionary, "angmsd;", HtmlEntityService.Convert(8737));
			HtmlEntityService.AddSingle(dictionary, "angmsdaa;", HtmlEntityService.Convert(10664));
			HtmlEntityService.AddSingle(dictionary, "angmsdab;", HtmlEntityService.Convert(10665));
			HtmlEntityService.AddSingle(dictionary, "angmsdac;", HtmlEntityService.Convert(10666));
			HtmlEntityService.AddSingle(dictionary, "angmsdad;", HtmlEntityService.Convert(10667));
			HtmlEntityService.AddSingle(dictionary, "angmsdae;", HtmlEntityService.Convert(10668));
			HtmlEntityService.AddSingle(dictionary, "angmsdaf;", HtmlEntityService.Convert(10669));
			HtmlEntityService.AddSingle(dictionary, "angmsdag;", HtmlEntityService.Convert(10670));
			HtmlEntityService.AddSingle(dictionary, "angmsdah;", HtmlEntityService.Convert(10671));
			HtmlEntityService.AddSingle(dictionary, "angrt;", HtmlEntityService.Convert(8735));
			HtmlEntityService.AddSingle(dictionary, "angrtvb;", HtmlEntityService.Convert(8894));
			HtmlEntityService.AddSingle(dictionary, "angrtvbd;", HtmlEntityService.Convert(10653));
			HtmlEntityService.AddSingle(dictionary, "angsph;", HtmlEntityService.Convert(8738));
			HtmlEntityService.AddSingle(dictionary, "angst;", HtmlEntityService.Convert(197));
			HtmlEntityService.AddSingle(dictionary, "angzarr;", HtmlEntityService.Convert(9084));
			HtmlEntityService.AddSingle(dictionary, "aogon;", HtmlEntityService.Convert(261));
			HtmlEntityService.AddSingle(dictionary, "aopf;", HtmlEntityService.Convert(120146));
			HtmlEntityService.AddSingle(dictionary, "ap;", HtmlEntityService.Convert(8776));
			HtmlEntityService.AddSingle(dictionary, "apacir;", HtmlEntityService.Convert(10863));
			HtmlEntityService.AddSingle(dictionary, "apE;", HtmlEntityService.Convert(10864));
			HtmlEntityService.AddSingle(dictionary, "ape;", HtmlEntityService.Convert(8778));
			HtmlEntityService.AddSingle(dictionary, "apid;", HtmlEntityService.Convert(8779));
			HtmlEntityService.AddSingle(dictionary, "apos;", HtmlEntityService.Convert(39));
			HtmlEntityService.AddSingle(dictionary, "approx;", HtmlEntityService.Convert(8776));
			HtmlEntityService.AddSingle(dictionary, "approxeq;", HtmlEntityService.Convert(8778));
			HtmlEntityService.AddBoth(dictionary, "aring;", HtmlEntityService.Convert(229));
			HtmlEntityService.AddSingle(dictionary, "ascr;", HtmlEntityService.Convert(119990));
			HtmlEntityService.AddSingle(dictionary, "ast;", HtmlEntityService.Convert(42));
			HtmlEntityService.AddSingle(dictionary, "asymp;", HtmlEntityService.Convert(8776));
			HtmlEntityService.AddSingle(dictionary, "asympeq;", HtmlEntityService.Convert(8781));
			HtmlEntityService.AddBoth(dictionary, "atilde;", HtmlEntityService.Convert(227));
			HtmlEntityService.AddBoth(dictionary, "auml;", HtmlEntityService.Convert(228));
			HtmlEntityService.AddSingle(dictionary, "awconint;", HtmlEntityService.Convert(8755));
			HtmlEntityService.AddSingle(dictionary, "awint;", HtmlEntityService.Convert(10769));
			return dictionary;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000211AC File Offset: 0x0001F3AC
		private Dictionary<string, string> GetSymbolBigA()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Aogon;", HtmlEntityService.Convert(260));
			HtmlEntityService.AddSingle(dictionary, "Aopf;", HtmlEntityService.Convert(120120));
			HtmlEntityService.AddSingle(dictionary, "ApplyFunction;", HtmlEntityService.Convert(8289));
			HtmlEntityService.AddBoth(dictionary, "Aring;", HtmlEntityService.Convert(197));
			HtmlEntityService.AddSingle(dictionary, "Ascr;", HtmlEntityService.Convert(119964));
			HtmlEntityService.AddSingle(dictionary, "Assign;", HtmlEntityService.Convert(8788));
			HtmlEntityService.AddBoth(dictionary, "Atilde;", HtmlEntityService.Convert(195));
			HtmlEntityService.AddBoth(dictionary, "Auml;", HtmlEntityService.Convert(196));
			HtmlEntityService.AddBoth(dictionary, "Aacute;", HtmlEntityService.Convert(193));
			HtmlEntityService.AddSingle(dictionary, "Abreve;", HtmlEntityService.Convert(258));
			HtmlEntityService.AddBoth(dictionary, "Acirc;", HtmlEntityService.Convert(194));
			HtmlEntityService.AddSingle(dictionary, "Acy;", HtmlEntityService.Convert(1040));
			HtmlEntityService.AddBoth(dictionary, "AElig;", HtmlEntityService.Convert(198));
			HtmlEntityService.AddSingle(dictionary, "Afr;", HtmlEntityService.Convert(120068));
			HtmlEntityService.AddBoth(dictionary, "Agrave;", HtmlEntityService.Convert(192));
			HtmlEntityService.AddSingle(dictionary, "Alpha;", HtmlEntityService.Convert(913));
			HtmlEntityService.AddSingle(dictionary, "Amacr;", HtmlEntityService.Convert(256));
			HtmlEntityService.AddBoth(dictionary, "AMP;", HtmlEntityService.Convert(38));
			HtmlEntityService.AddSingle(dictionary, "And;", HtmlEntityService.Convert(10835));
			return dictionary;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002134C File Offset: 0x0001F54C
		private Dictionary<string, string> GetSymbolLittleB()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "backcong;", HtmlEntityService.Convert(8780));
			HtmlEntityService.AddSingle(dictionary, "backepsilon;", HtmlEntityService.Convert(1014));
			HtmlEntityService.AddSingle(dictionary, "backprime;", HtmlEntityService.Convert(8245));
			HtmlEntityService.AddSingle(dictionary, "backsim;", HtmlEntityService.Convert(8765));
			HtmlEntityService.AddSingle(dictionary, "backsimeq;", HtmlEntityService.Convert(8909));
			HtmlEntityService.AddSingle(dictionary, "barvee;", HtmlEntityService.Convert(8893));
			HtmlEntityService.AddSingle(dictionary, "barwed;", HtmlEntityService.Convert(8965));
			HtmlEntityService.AddSingle(dictionary, "barwedge;", HtmlEntityService.Convert(8965));
			HtmlEntityService.AddSingle(dictionary, "bbrk;", HtmlEntityService.Convert(9141));
			HtmlEntityService.AddSingle(dictionary, "bbrktbrk;", HtmlEntityService.Convert(9142));
			HtmlEntityService.AddSingle(dictionary, "bcong;", HtmlEntityService.Convert(8780));
			HtmlEntityService.AddSingle(dictionary, "bcy;", HtmlEntityService.Convert(1073));
			HtmlEntityService.AddSingle(dictionary, "bdquo;", HtmlEntityService.Convert(8222));
			HtmlEntityService.AddSingle(dictionary, "becaus;", HtmlEntityService.Convert(8757));
			HtmlEntityService.AddSingle(dictionary, "because;", HtmlEntityService.Convert(8757));
			HtmlEntityService.AddSingle(dictionary, "bemptyv;", HtmlEntityService.Convert(10672));
			HtmlEntityService.AddSingle(dictionary, "bepsi;", HtmlEntityService.Convert(1014));
			HtmlEntityService.AddSingle(dictionary, "bernou;", HtmlEntityService.Convert(8492));
			HtmlEntityService.AddSingle(dictionary, "beta;", HtmlEntityService.Convert(946));
			HtmlEntityService.AddSingle(dictionary, "beth;", HtmlEntityService.Convert(8502));
			HtmlEntityService.AddSingle(dictionary, "between;", HtmlEntityService.Convert(8812));
			HtmlEntityService.AddSingle(dictionary, "bfr;", HtmlEntityService.Convert(120095));
			HtmlEntityService.AddSingle(dictionary, "bigcap;", HtmlEntityService.Convert(8898));
			HtmlEntityService.AddSingle(dictionary, "bigcirc;", HtmlEntityService.Convert(9711));
			HtmlEntityService.AddSingle(dictionary, "bigcup;", HtmlEntityService.Convert(8899));
			HtmlEntityService.AddSingle(dictionary, "bigodot;", HtmlEntityService.Convert(10752));
			HtmlEntityService.AddSingle(dictionary, "bigoplus;", HtmlEntityService.Convert(10753));
			HtmlEntityService.AddSingle(dictionary, "bigotimes;", HtmlEntityService.Convert(10754));
			HtmlEntityService.AddSingle(dictionary, "bigsqcup;", HtmlEntityService.Convert(10758));
			HtmlEntityService.AddSingle(dictionary, "bigstar;", HtmlEntityService.Convert(9733));
			HtmlEntityService.AddSingle(dictionary, "bigtriangledown;", HtmlEntityService.Convert(9661));
			HtmlEntityService.AddSingle(dictionary, "bigtriangleup;", HtmlEntityService.Convert(9651));
			HtmlEntityService.AddSingle(dictionary, "biguplus;", HtmlEntityService.Convert(10756));
			HtmlEntityService.AddSingle(dictionary, "bigvee;", HtmlEntityService.Convert(8897));
			HtmlEntityService.AddSingle(dictionary, "bigwedge;", HtmlEntityService.Convert(8896));
			HtmlEntityService.AddSingle(dictionary, "bkarow;", HtmlEntityService.Convert(10509));
			HtmlEntityService.AddSingle(dictionary, "blacklozenge;", HtmlEntityService.Convert(10731));
			HtmlEntityService.AddSingle(dictionary, "blacksquare;", HtmlEntityService.Convert(9642));
			HtmlEntityService.AddSingle(dictionary, "blacktriangle;", HtmlEntityService.Convert(9652));
			HtmlEntityService.AddSingle(dictionary, "blacktriangledown;", HtmlEntityService.Convert(9662));
			HtmlEntityService.AddSingle(dictionary, "blacktriangleleft;", HtmlEntityService.Convert(9666));
			HtmlEntityService.AddSingle(dictionary, "blacktriangleright;", HtmlEntityService.Convert(9656));
			HtmlEntityService.AddSingle(dictionary, "blank;", HtmlEntityService.Convert(9251));
			HtmlEntityService.AddSingle(dictionary, "blk12;", HtmlEntityService.Convert(9618));
			HtmlEntityService.AddSingle(dictionary, "blk14;", HtmlEntityService.Convert(9617));
			HtmlEntityService.AddSingle(dictionary, "blk34;", HtmlEntityService.Convert(9619));
			HtmlEntityService.AddSingle(dictionary, "block;", HtmlEntityService.Convert(9608));
			HtmlEntityService.AddSingle(dictionary, "bne;", HtmlEntityService.Convert(61, 8421));
			HtmlEntityService.AddSingle(dictionary, "bnequiv;", HtmlEntityService.Convert(8801, 8421));
			HtmlEntityService.AddSingle(dictionary, "bNot;", HtmlEntityService.Convert(10989));
			HtmlEntityService.AddSingle(dictionary, "bnot;", HtmlEntityService.Convert(8976));
			HtmlEntityService.AddSingle(dictionary, "bopf;", HtmlEntityService.Convert(120147));
			HtmlEntityService.AddSingle(dictionary, "bot;", HtmlEntityService.Convert(8869));
			HtmlEntityService.AddSingle(dictionary, "bottom;", HtmlEntityService.Convert(8869));
			HtmlEntityService.AddSingle(dictionary, "bowtie;", HtmlEntityService.Convert(8904));
			HtmlEntityService.AddSingle(dictionary, "boxbox;", HtmlEntityService.Convert(10697));
			HtmlEntityService.AddSingle(dictionary, "boxDL;", HtmlEntityService.Convert(9559));
			HtmlEntityService.AddSingle(dictionary, "boxDl;", HtmlEntityService.Convert(9558));
			HtmlEntityService.AddSingle(dictionary, "boxdL;", HtmlEntityService.Convert(9557));
			HtmlEntityService.AddSingle(dictionary, "boxdl;", HtmlEntityService.Convert(9488));
			HtmlEntityService.AddSingle(dictionary, "boxDR;", HtmlEntityService.Convert(9556));
			HtmlEntityService.AddSingle(dictionary, "boxDr;", HtmlEntityService.Convert(9555));
			HtmlEntityService.AddSingle(dictionary, "boxdR;", HtmlEntityService.Convert(9554));
			HtmlEntityService.AddSingle(dictionary, "boxdr;", HtmlEntityService.Convert(9484));
			HtmlEntityService.AddSingle(dictionary, "boxH;", HtmlEntityService.Convert(9552));
			HtmlEntityService.AddSingle(dictionary, "boxh;", HtmlEntityService.Convert(9472));
			HtmlEntityService.AddSingle(dictionary, "boxHD;", HtmlEntityService.Convert(9574));
			HtmlEntityService.AddSingle(dictionary, "boxHd;", HtmlEntityService.Convert(9572));
			HtmlEntityService.AddSingle(dictionary, "boxhD;", HtmlEntityService.Convert(9573));
			HtmlEntityService.AddSingle(dictionary, "boxhd;", HtmlEntityService.Convert(9516));
			HtmlEntityService.AddSingle(dictionary, "boxHU;", HtmlEntityService.Convert(9577));
			HtmlEntityService.AddSingle(dictionary, "boxHu;", HtmlEntityService.Convert(9575));
			HtmlEntityService.AddSingle(dictionary, "boxhU;", HtmlEntityService.Convert(9576));
			HtmlEntityService.AddSingle(dictionary, "boxhu;", HtmlEntityService.Convert(9524));
			HtmlEntityService.AddSingle(dictionary, "boxminus;", HtmlEntityService.Convert(8863));
			HtmlEntityService.AddSingle(dictionary, "boxplus;", HtmlEntityService.Convert(8862));
			HtmlEntityService.AddSingle(dictionary, "boxtimes;", HtmlEntityService.Convert(8864));
			HtmlEntityService.AddSingle(dictionary, "boxUL;", HtmlEntityService.Convert(9565));
			HtmlEntityService.AddSingle(dictionary, "boxUl;", HtmlEntityService.Convert(9564));
			HtmlEntityService.AddSingle(dictionary, "boxuL;", HtmlEntityService.Convert(9563));
			HtmlEntityService.AddSingle(dictionary, "boxul;", HtmlEntityService.Convert(9496));
			HtmlEntityService.AddSingle(dictionary, "boxUR;", HtmlEntityService.Convert(9562));
			HtmlEntityService.AddSingle(dictionary, "boxUr;", HtmlEntityService.Convert(9561));
			HtmlEntityService.AddSingle(dictionary, "boxuR;", HtmlEntityService.Convert(9560));
			HtmlEntityService.AddSingle(dictionary, "boxur;", HtmlEntityService.Convert(9492));
			HtmlEntityService.AddSingle(dictionary, "boxV;", HtmlEntityService.Convert(9553));
			HtmlEntityService.AddSingle(dictionary, "boxv;", HtmlEntityService.Convert(9474));
			HtmlEntityService.AddSingle(dictionary, "boxVH;", HtmlEntityService.Convert(9580));
			HtmlEntityService.AddSingle(dictionary, "boxVh;", HtmlEntityService.Convert(9579));
			HtmlEntityService.AddSingle(dictionary, "boxvH;", HtmlEntityService.Convert(9578));
			HtmlEntityService.AddSingle(dictionary, "boxvh;", HtmlEntityService.Convert(9532));
			HtmlEntityService.AddSingle(dictionary, "boxVL;", HtmlEntityService.Convert(9571));
			HtmlEntityService.AddSingle(dictionary, "boxVl;", HtmlEntityService.Convert(9570));
			HtmlEntityService.AddSingle(dictionary, "boxvL;", HtmlEntityService.Convert(9569));
			HtmlEntityService.AddSingle(dictionary, "boxvl;", HtmlEntityService.Convert(9508));
			HtmlEntityService.AddSingle(dictionary, "boxVR;", HtmlEntityService.Convert(9568));
			HtmlEntityService.AddSingle(dictionary, "boxVr;", HtmlEntityService.Convert(9567));
			HtmlEntityService.AddSingle(dictionary, "boxvR;", HtmlEntityService.Convert(9566));
			HtmlEntityService.AddSingle(dictionary, "boxvr;", HtmlEntityService.Convert(9500));
			HtmlEntityService.AddSingle(dictionary, "bprime;", HtmlEntityService.Convert(8245));
			HtmlEntityService.AddSingle(dictionary, "breve;", HtmlEntityService.Convert(728));
			HtmlEntityService.AddBoth(dictionary, "brvbar;", HtmlEntityService.Convert(166));
			HtmlEntityService.AddSingle(dictionary, "bscr;", HtmlEntityService.Convert(119991));
			HtmlEntityService.AddSingle(dictionary, "bsemi;", HtmlEntityService.Convert(8271));
			HtmlEntityService.AddSingle(dictionary, "bsim;", HtmlEntityService.Convert(8765));
			HtmlEntityService.AddSingle(dictionary, "bsime;", HtmlEntityService.Convert(8909));
			HtmlEntityService.AddSingle(dictionary, "bsol;", HtmlEntityService.Convert(92));
			HtmlEntityService.AddSingle(dictionary, "bsolb;", HtmlEntityService.Convert(10693));
			HtmlEntityService.AddSingle(dictionary, "bsolhsub;", HtmlEntityService.Convert(10184));
			HtmlEntityService.AddSingle(dictionary, "bull;", HtmlEntityService.Convert(8226));
			HtmlEntityService.AddSingle(dictionary, "bullet;", HtmlEntityService.Convert(8226));
			HtmlEntityService.AddSingle(dictionary, "bump;", HtmlEntityService.Convert(8782));
			HtmlEntityService.AddSingle(dictionary, "bumpE;", HtmlEntityService.Convert(10926));
			HtmlEntityService.AddSingle(dictionary, "bumpe;", HtmlEntityService.Convert(8783));
			HtmlEntityService.AddSingle(dictionary, "bumpeq;", HtmlEntityService.Convert(8783));
			return dictionary;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00021CD4 File Offset: 0x0001FED4
		private Dictionary<string, string> GetSymbolBigB()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Backslash;", HtmlEntityService.Convert(8726));
			HtmlEntityService.AddSingle(dictionary, "Barv;", HtmlEntityService.Convert(10983));
			HtmlEntityService.AddSingle(dictionary, "Barwed;", HtmlEntityService.Convert(8966));
			HtmlEntityService.AddSingle(dictionary, "Bcy;", HtmlEntityService.Convert(1041));
			HtmlEntityService.AddSingle(dictionary, "Because;", HtmlEntityService.Convert(8757));
			HtmlEntityService.AddSingle(dictionary, "Bernoullis;", HtmlEntityService.Convert(8492));
			HtmlEntityService.AddSingle(dictionary, "Beta;", HtmlEntityService.Convert(914));
			HtmlEntityService.AddSingle(dictionary, "Bfr;", HtmlEntityService.Convert(120069));
			HtmlEntityService.AddSingle(dictionary, "Bopf;", HtmlEntityService.Convert(120121));
			HtmlEntityService.AddSingle(dictionary, "Breve;", HtmlEntityService.Convert(728));
			HtmlEntityService.AddSingle(dictionary, "Bscr;", HtmlEntityService.Convert(8492));
			HtmlEntityService.AddSingle(dictionary, "Bumpeq;", HtmlEntityService.Convert(8782));
			return dictionary;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00021DE4 File Offset: 0x0001FFE4
		private Dictionary<string, string> GetSymbolLittleC()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "cacute;", HtmlEntityService.Convert(263));
			HtmlEntityService.AddSingle(dictionary, "cap;", HtmlEntityService.Convert(8745));
			HtmlEntityService.AddSingle(dictionary, "capand;", HtmlEntityService.Convert(10820));
			HtmlEntityService.AddSingle(dictionary, "capbrcup;", HtmlEntityService.Convert(10825));
			HtmlEntityService.AddSingle(dictionary, "capcap;", HtmlEntityService.Convert(10827));
			HtmlEntityService.AddSingle(dictionary, "capcup;", HtmlEntityService.Convert(10823));
			HtmlEntityService.AddSingle(dictionary, "capdot;", HtmlEntityService.Convert(10816));
			HtmlEntityService.AddSingle(dictionary, "caps;", HtmlEntityService.Convert(8745, 65024));
			HtmlEntityService.AddSingle(dictionary, "caret;", HtmlEntityService.Convert(8257));
			HtmlEntityService.AddSingle(dictionary, "caron;", HtmlEntityService.Convert(711));
			HtmlEntityService.AddSingle(dictionary, "ccaps;", HtmlEntityService.Convert(10829));
			HtmlEntityService.AddSingle(dictionary, "ccaron;", HtmlEntityService.Convert(269));
			HtmlEntityService.AddBoth(dictionary, "ccedil;", HtmlEntityService.Convert(231));
			HtmlEntityService.AddSingle(dictionary, "ccirc;", HtmlEntityService.Convert(265));
			HtmlEntityService.AddSingle(dictionary, "ccups;", HtmlEntityService.Convert(10828));
			HtmlEntityService.AddSingle(dictionary, "ccupssm;", HtmlEntityService.Convert(10832));
			HtmlEntityService.AddSingle(dictionary, "cdot;", HtmlEntityService.Convert(267));
			HtmlEntityService.AddBoth(dictionary, "cedil;", HtmlEntityService.Convert(184));
			HtmlEntityService.AddSingle(dictionary, "cemptyv;", HtmlEntityService.Convert(10674));
			HtmlEntityService.AddBoth(dictionary, "cent;", HtmlEntityService.Convert(162));
			HtmlEntityService.AddSingle(dictionary, "centerdot;", HtmlEntityService.Convert(183));
			HtmlEntityService.AddSingle(dictionary, "cfr;", HtmlEntityService.Convert(120096));
			HtmlEntityService.AddSingle(dictionary, "chcy;", HtmlEntityService.Convert(1095));
			HtmlEntityService.AddSingle(dictionary, "check;", HtmlEntityService.Convert(10003));
			HtmlEntityService.AddSingle(dictionary, "checkmark;", HtmlEntityService.Convert(10003));
			HtmlEntityService.AddSingle(dictionary, "chi;", HtmlEntityService.Convert(967));
			HtmlEntityService.AddSingle(dictionary, "cir;", HtmlEntityService.Convert(9675));
			HtmlEntityService.AddSingle(dictionary, "circ;", HtmlEntityService.Convert(710));
			HtmlEntityService.AddSingle(dictionary, "circeq;", HtmlEntityService.Convert(8791));
			HtmlEntityService.AddSingle(dictionary, "circlearrowleft;", HtmlEntityService.Convert(8634));
			HtmlEntityService.AddSingle(dictionary, "circlearrowright;", HtmlEntityService.Convert(8635));
			HtmlEntityService.AddSingle(dictionary, "circledast;", HtmlEntityService.Convert(8859));
			HtmlEntityService.AddSingle(dictionary, "circledcirc;", HtmlEntityService.Convert(8858));
			HtmlEntityService.AddSingle(dictionary, "circleddash;", HtmlEntityService.Convert(8861));
			HtmlEntityService.AddSingle(dictionary, "circledR;", HtmlEntityService.Convert(174));
			HtmlEntityService.AddSingle(dictionary, "circledS;", HtmlEntityService.Convert(9416));
			HtmlEntityService.AddSingle(dictionary, "cirE;", HtmlEntityService.Convert(10691));
			HtmlEntityService.AddSingle(dictionary, "cire;", HtmlEntityService.Convert(8791));
			HtmlEntityService.AddSingle(dictionary, "cirfnint;", HtmlEntityService.Convert(10768));
			HtmlEntityService.AddSingle(dictionary, "cirmid;", HtmlEntityService.Convert(10991));
			HtmlEntityService.AddSingle(dictionary, "cirscir;", HtmlEntityService.Convert(10690));
			HtmlEntityService.AddSingle(dictionary, "clubs;", HtmlEntityService.Convert(9827));
			HtmlEntityService.AddSingle(dictionary, "clubsuit;", HtmlEntityService.Convert(9827));
			HtmlEntityService.AddSingle(dictionary, "colon;", HtmlEntityService.Convert(58));
			HtmlEntityService.AddSingle(dictionary, "colone;", HtmlEntityService.Convert(8788));
			HtmlEntityService.AddSingle(dictionary, "coloneq;", HtmlEntityService.Convert(8788));
			HtmlEntityService.AddSingle(dictionary, "comma;", HtmlEntityService.Convert(44));
			HtmlEntityService.AddSingle(dictionary, "commat;", HtmlEntityService.Convert(64));
			HtmlEntityService.AddSingle(dictionary, "comp;", HtmlEntityService.Convert(8705));
			HtmlEntityService.AddSingle(dictionary, "compfn;", HtmlEntityService.Convert(8728));
			HtmlEntityService.AddSingle(dictionary, "complement;", HtmlEntityService.Convert(8705));
			HtmlEntityService.AddSingle(dictionary, "complexes;", HtmlEntityService.Convert(8450));
			HtmlEntityService.AddSingle(dictionary, "cong;", HtmlEntityService.Convert(8773));
			HtmlEntityService.AddSingle(dictionary, "congdot;", HtmlEntityService.Convert(10861));
			HtmlEntityService.AddSingle(dictionary, "conint;", HtmlEntityService.Convert(8750));
			HtmlEntityService.AddSingle(dictionary, "copf;", HtmlEntityService.Convert(120148));
			HtmlEntityService.AddSingle(dictionary, "coprod;", HtmlEntityService.Convert(8720));
			HtmlEntityService.AddBoth(dictionary, "copy;", HtmlEntityService.Convert(169));
			HtmlEntityService.AddSingle(dictionary, "copysr;", HtmlEntityService.Convert(8471));
			HtmlEntityService.AddSingle(dictionary, "crarr;", HtmlEntityService.Convert(8629));
			HtmlEntityService.AddSingle(dictionary, "cross;", HtmlEntityService.Convert(10007));
			HtmlEntityService.AddSingle(dictionary, "cscr;", HtmlEntityService.Convert(119992));
			HtmlEntityService.AddSingle(dictionary, "csub;", HtmlEntityService.Convert(10959));
			HtmlEntityService.AddSingle(dictionary, "csube;", HtmlEntityService.Convert(10961));
			HtmlEntityService.AddSingle(dictionary, "csup;", HtmlEntityService.Convert(10960));
			HtmlEntityService.AddSingle(dictionary, "csupe;", HtmlEntityService.Convert(10962));
			HtmlEntityService.AddSingle(dictionary, "ctdot;", HtmlEntityService.Convert(8943));
			HtmlEntityService.AddSingle(dictionary, "cudarrl;", HtmlEntityService.Convert(10552));
			HtmlEntityService.AddSingle(dictionary, "cudarrr;", HtmlEntityService.Convert(10549));
			HtmlEntityService.AddSingle(dictionary, "cuepr;", HtmlEntityService.Convert(8926));
			HtmlEntityService.AddSingle(dictionary, "cuesc;", HtmlEntityService.Convert(8927));
			HtmlEntityService.AddSingle(dictionary, "cularr;", HtmlEntityService.Convert(8630));
			HtmlEntityService.AddSingle(dictionary, "cularrp;", HtmlEntityService.Convert(10557));
			HtmlEntityService.AddSingle(dictionary, "cup;", HtmlEntityService.Convert(8746));
			HtmlEntityService.AddSingle(dictionary, "cupbrcap;", HtmlEntityService.Convert(10824));
			HtmlEntityService.AddSingle(dictionary, "cupcap;", HtmlEntityService.Convert(10822));
			HtmlEntityService.AddSingle(dictionary, "cupcup;", HtmlEntityService.Convert(10826));
			HtmlEntityService.AddSingle(dictionary, "cupdot;", HtmlEntityService.Convert(8845));
			HtmlEntityService.AddSingle(dictionary, "cupor;", HtmlEntityService.Convert(10821));
			HtmlEntityService.AddSingle(dictionary, "cups;", HtmlEntityService.Convert(8746, 65024));
			HtmlEntityService.AddSingle(dictionary, "curarr;", HtmlEntityService.Convert(8631));
			HtmlEntityService.AddSingle(dictionary, "curarrm;", HtmlEntityService.Convert(10556));
			HtmlEntityService.AddSingle(dictionary, "curlyeqprec;", HtmlEntityService.Convert(8926));
			HtmlEntityService.AddSingle(dictionary, "curlyeqsucc;", HtmlEntityService.Convert(8927));
			HtmlEntityService.AddSingle(dictionary, "curlyvee;", HtmlEntityService.Convert(8910));
			HtmlEntityService.AddSingle(dictionary, "curlywedge;", HtmlEntityService.Convert(8911));
			HtmlEntityService.AddBoth(dictionary, "curren;", HtmlEntityService.Convert(164));
			HtmlEntityService.AddSingle(dictionary, "curvearrowleft;", HtmlEntityService.Convert(8630));
			HtmlEntityService.AddSingle(dictionary, "curvearrowright;", HtmlEntityService.Convert(8631));
			HtmlEntityService.AddSingle(dictionary, "cuvee;", HtmlEntityService.Convert(8910));
			HtmlEntityService.AddSingle(dictionary, "cuwed;", HtmlEntityService.Convert(8911));
			HtmlEntityService.AddSingle(dictionary, "cwconint;", HtmlEntityService.Convert(8754));
			HtmlEntityService.AddSingle(dictionary, "cwint;", HtmlEntityService.Convert(8753));
			HtmlEntityService.AddSingle(dictionary, "cylcty;", HtmlEntityService.Convert(9005));
			return dictionary;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000225B0 File Offset: 0x000207B0
		private Dictionary<string, string> GetSymbolBigC()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Cacute;", HtmlEntityService.Convert(262));
			HtmlEntityService.AddSingle(dictionary, "Cap;", HtmlEntityService.Convert(8914));
			HtmlEntityService.AddSingle(dictionary, "CapitalDifferentialD;", HtmlEntityService.Convert(8517));
			HtmlEntityService.AddSingle(dictionary, "Cayleys;", HtmlEntityService.Convert(8493));
			HtmlEntityService.AddSingle(dictionary, "Ccaron;", HtmlEntityService.Convert(268));
			HtmlEntityService.AddBoth(dictionary, "Ccedil;", HtmlEntityService.Convert(199));
			HtmlEntityService.AddSingle(dictionary, "Ccirc;", HtmlEntityService.Convert(264));
			HtmlEntityService.AddSingle(dictionary, "Cconint;", HtmlEntityService.Convert(8752));
			HtmlEntityService.AddSingle(dictionary, "Cdot;", HtmlEntityService.Convert(266));
			HtmlEntityService.AddSingle(dictionary, "Cedilla;", HtmlEntityService.Convert(184));
			HtmlEntityService.AddSingle(dictionary, "CenterDot;", HtmlEntityService.Convert(183));
			HtmlEntityService.AddSingle(dictionary, "Cfr;", HtmlEntityService.Convert(8493));
			HtmlEntityService.AddSingle(dictionary, "CHcy;", HtmlEntityService.Convert(1063));
			HtmlEntityService.AddSingle(dictionary, "Chi;", HtmlEntityService.Convert(935));
			HtmlEntityService.AddSingle(dictionary, "CircleDot;", HtmlEntityService.Convert(8857));
			HtmlEntityService.AddSingle(dictionary, "CircleMinus;", HtmlEntityService.Convert(8854));
			HtmlEntityService.AddSingle(dictionary, "CirclePlus;", HtmlEntityService.Convert(8853));
			HtmlEntityService.AddSingle(dictionary, "CircleTimes;", HtmlEntityService.Convert(8855));
			HtmlEntityService.AddSingle(dictionary, "ClockwiseContourIntegral;", HtmlEntityService.Convert(8754));
			HtmlEntityService.AddSingle(dictionary, "CloseCurlyDoubleQuote;", HtmlEntityService.Convert(8221));
			HtmlEntityService.AddSingle(dictionary, "CloseCurlyQuote;", HtmlEntityService.Convert(8217));
			HtmlEntityService.AddSingle(dictionary, "Colon;", HtmlEntityService.Convert(8759));
			HtmlEntityService.AddSingle(dictionary, "Colone;", HtmlEntityService.Convert(10868));
			HtmlEntityService.AddSingle(dictionary, "Congruent;", HtmlEntityService.Convert(8801));
			HtmlEntityService.AddSingle(dictionary, "Conint;", HtmlEntityService.Convert(8751));
			HtmlEntityService.AddSingle(dictionary, "ContourIntegral;", HtmlEntityService.Convert(8750));
			HtmlEntityService.AddSingle(dictionary, "Copf;", HtmlEntityService.Convert(8450));
			HtmlEntityService.AddSingle(dictionary, "Coproduct;", HtmlEntityService.Convert(8720));
			HtmlEntityService.AddBoth(dictionary, "COPY;", HtmlEntityService.Convert(169));
			HtmlEntityService.AddSingle(dictionary, "CounterClockwiseContourIntegral;", HtmlEntityService.Convert(8755));
			HtmlEntityService.AddSingle(dictionary, "Cross;", HtmlEntityService.Convert(10799));
			HtmlEntityService.AddSingle(dictionary, "Cscr;", HtmlEntityService.Convert(119966));
			HtmlEntityService.AddSingle(dictionary, "Cup;", HtmlEntityService.Convert(8915));
			HtmlEntityService.AddSingle(dictionary, "CupCap;", HtmlEntityService.Convert(8781));
			return dictionary;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0002288C File Offset: 0x00020A8C
		private Dictionary<string, string> GetSymbolLittleD()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "dagger;", HtmlEntityService.Convert(8224));
			HtmlEntityService.AddSingle(dictionary, "daleth;", HtmlEntityService.Convert(8504));
			HtmlEntityService.AddSingle(dictionary, "dArr;", HtmlEntityService.Convert(8659));
			HtmlEntityService.AddSingle(dictionary, "darr;", HtmlEntityService.Convert(8595));
			HtmlEntityService.AddSingle(dictionary, "dash;", HtmlEntityService.Convert(8208));
			HtmlEntityService.AddSingle(dictionary, "dashv;", HtmlEntityService.Convert(8867));
			HtmlEntityService.AddSingle(dictionary, "dbkarow;", HtmlEntityService.Convert(10511));
			HtmlEntityService.AddSingle(dictionary, "dblac;", HtmlEntityService.Convert(733));
			HtmlEntityService.AddSingle(dictionary, "dcaron;", HtmlEntityService.Convert(271));
			HtmlEntityService.AddSingle(dictionary, "dcy;", HtmlEntityService.Convert(1076));
			HtmlEntityService.AddSingle(dictionary, "dd;", HtmlEntityService.Convert(8518));
			HtmlEntityService.AddSingle(dictionary, "ddagger;", HtmlEntityService.Convert(8225));
			HtmlEntityService.AddSingle(dictionary, "ddarr;", HtmlEntityService.Convert(8650));
			HtmlEntityService.AddSingle(dictionary, "ddotseq;", HtmlEntityService.Convert(10871));
			HtmlEntityService.AddBoth(dictionary, "deg;", HtmlEntityService.Convert(176));
			HtmlEntityService.AddSingle(dictionary, "delta;", HtmlEntityService.Convert(948));
			HtmlEntityService.AddSingle(dictionary, "demptyv;", HtmlEntityService.Convert(10673));
			HtmlEntityService.AddSingle(dictionary, "dfisht;", HtmlEntityService.Convert(10623));
			HtmlEntityService.AddSingle(dictionary, "dfr;", HtmlEntityService.Convert(120097));
			HtmlEntityService.AddSingle(dictionary, "dHar;", HtmlEntityService.Convert(10597));
			HtmlEntityService.AddSingle(dictionary, "dharl;", HtmlEntityService.Convert(8643));
			HtmlEntityService.AddSingle(dictionary, "dharr;", HtmlEntityService.Convert(8642));
			HtmlEntityService.AddSingle(dictionary, "diam;", HtmlEntityService.Convert(8900));
			HtmlEntityService.AddSingle(dictionary, "diamond;", HtmlEntityService.Convert(8900));
			HtmlEntityService.AddSingle(dictionary, "diamondsuit;", HtmlEntityService.Convert(9830));
			HtmlEntityService.AddSingle(dictionary, "diams;", HtmlEntityService.Convert(9830));
			HtmlEntityService.AddSingle(dictionary, "die;", HtmlEntityService.Convert(168));
			HtmlEntityService.AddSingle(dictionary, "digamma;", HtmlEntityService.Convert(989));
			HtmlEntityService.AddSingle(dictionary, "disin;", HtmlEntityService.Convert(8946));
			HtmlEntityService.AddSingle(dictionary, "div;", HtmlEntityService.Convert(247));
			HtmlEntityService.AddBoth(dictionary, "divide;", HtmlEntityService.Convert(247));
			HtmlEntityService.AddSingle(dictionary, "divideontimes;", HtmlEntityService.Convert(8903));
			HtmlEntityService.AddSingle(dictionary, "divonx;", HtmlEntityService.Convert(8903));
			HtmlEntityService.AddSingle(dictionary, "djcy;", HtmlEntityService.Convert(1106));
			HtmlEntityService.AddSingle(dictionary, "dlcorn;", HtmlEntityService.Convert(8990));
			HtmlEntityService.AddSingle(dictionary, "dlcrop;", HtmlEntityService.Convert(8973));
			HtmlEntityService.AddSingle(dictionary, "dollar;", HtmlEntityService.Convert(36));
			HtmlEntityService.AddSingle(dictionary, "dopf;", HtmlEntityService.Convert(120149));
			HtmlEntityService.AddSingle(dictionary, "dot;", HtmlEntityService.Convert(729));
			HtmlEntityService.AddSingle(dictionary, "doteq;", HtmlEntityService.Convert(8784));
			HtmlEntityService.AddSingle(dictionary, "doteqdot;", HtmlEntityService.Convert(8785));
			HtmlEntityService.AddSingle(dictionary, "dotminus;", HtmlEntityService.Convert(8760));
			HtmlEntityService.AddSingle(dictionary, "dotplus;", HtmlEntityService.Convert(8724));
			HtmlEntityService.AddSingle(dictionary, "dotsquare;", HtmlEntityService.Convert(8865));
			HtmlEntityService.AddSingle(dictionary, "doublebarwedge;", HtmlEntityService.Convert(8966));
			HtmlEntityService.AddSingle(dictionary, "downarrow;", HtmlEntityService.Convert(8595));
			HtmlEntityService.AddSingle(dictionary, "downdownarrows;", HtmlEntityService.Convert(8650));
			HtmlEntityService.AddSingle(dictionary, "downharpoonleft;", HtmlEntityService.Convert(8643));
			HtmlEntityService.AddSingle(dictionary, "downharpoonright;", HtmlEntityService.Convert(8642));
			HtmlEntityService.AddSingle(dictionary, "drbkarow;", HtmlEntityService.Convert(10512));
			HtmlEntityService.AddSingle(dictionary, "drcorn;", HtmlEntityService.Convert(8991));
			HtmlEntityService.AddSingle(dictionary, "drcrop;", HtmlEntityService.Convert(8972));
			HtmlEntityService.AddSingle(dictionary, "dscr;", HtmlEntityService.Convert(119993));
			HtmlEntityService.AddSingle(dictionary, "dscy;", HtmlEntityService.Convert(1109));
			HtmlEntityService.AddSingle(dictionary, "dsol;", HtmlEntityService.Convert(10742));
			HtmlEntityService.AddSingle(dictionary, "dstrok;", HtmlEntityService.Convert(273));
			HtmlEntityService.AddSingle(dictionary, "dtdot;", HtmlEntityService.Convert(8945));
			HtmlEntityService.AddSingle(dictionary, "dtri;", HtmlEntityService.Convert(9663));
			HtmlEntityService.AddSingle(dictionary, "dtrif;", HtmlEntityService.Convert(9662));
			HtmlEntityService.AddSingle(dictionary, "duarr;", HtmlEntityService.Convert(8693));
			HtmlEntityService.AddSingle(dictionary, "duhar;", HtmlEntityService.Convert(10607));
			HtmlEntityService.AddSingle(dictionary, "dwangle;", HtmlEntityService.Convert(10662));
			HtmlEntityService.AddSingle(dictionary, "dzcy;", HtmlEntityService.Convert(1119));
			HtmlEntityService.AddSingle(dictionary, "dzigrarr;", HtmlEntityService.Convert(10239));
			return dictionary;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00022DDC File Offset: 0x00020FDC
		private Dictionary<string, string> GetSymbolBigD()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Dagger;", HtmlEntityService.Convert(8225));
			HtmlEntityService.AddSingle(dictionary, "Darr;", HtmlEntityService.Convert(8609));
			HtmlEntityService.AddSingle(dictionary, "Dashv;", HtmlEntityService.Convert(10980));
			HtmlEntityService.AddSingle(dictionary, "Dcaron;", HtmlEntityService.Convert(270));
			HtmlEntityService.AddSingle(dictionary, "Dcy;", HtmlEntityService.Convert(1044));
			HtmlEntityService.AddSingle(dictionary, "DD;", HtmlEntityService.Convert(8517));
			HtmlEntityService.AddSingle(dictionary, "DDotrahd;", HtmlEntityService.Convert(10513));
			HtmlEntityService.AddSingle(dictionary, "Del;", HtmlEntityService.Convert(8711));
			HtmlEntityService.AddSingle(dictionary, "Delta;", HtmlEntityService.Convert(916));
			HtmlEntityService.AddSingle(dictionary, "Dfr;", HtmlEntityService.Convert(120071));
			HtmlEntityService.AddSingle(dictionary, "DiacriticalAcute;", HtmlEntityService.Convert(180));
			HtmlEntityService.AddSingle(dictionary, "DiacriticalDot;", HtmlEntityService.Convert(729));
			HtmlEntityService.AddSingle(dictionary, "DiacriticalDoubleAcute;", HtmlEntityService.Convert(733));
			HtmlEntityService.AddSingle(dictionary, "DiacriticalGrave;", HtmlEntityService.Convert(96));
			HtmlEntityService.AddSingle(dictionary, "DiacriticalTilde;", HtmlEntityService.Convert(732));
			HtmlEntityService.AddSingle(dictionary, "Diamond;", HtmlEntityService.Convert(8900));
			HtmlEntityService.AddSingle(dictionary, "DifferentialD;", HtmlEntityService.Convert(8518));
			HtmlEntityService.AddSingle(dictionary, "DJcy;", HtmlEntityService.Convert(1026));
			HtmlEntityService.AddSingle(dictionary, "Dopf;", HtmlEntityService.Convert(120123));
			HtmlEntityService.AddSingle(dictionary, "Dot;", HtmlEntityService.Convert(168));
			HtmlEntityService.AddSingle(dictionary, "DotDot;", HtmlEntityService.Convert(8412));
			HtmlEntityService.AddSingle(dictionary, "DotEqual;", HtmlEntityService.Convert(8784));
			HtmlEntityService.AddSingle(dictionary, "DoubleContourIntegral;", HtmlEntityService.Convert(8751));
			HtmlEntityService.AddSingle(dictionary, "DoubleDot;", HtmlEntityService.Convert(168));
			HtmlEntityService.AddSingle(dictionary, "DoubleDownArrow;", HtmlEntityService.Convert(8659));
			HtmlEntityService.AddSingle(dictionary, "DoubleLeftArrow;", HtmlEntityService.Convert(8656));
			HtmlEntityService.AddSingle(dictionary, "DoubleLeftRightArrow;", HtmlEntityService.Convert(8660));
			HtmlEntityService.AddSingle(dictionary, "DoubleLeftTee;", HtmlEntityService.Convert(10980));
			HtmlEntityService.AddSingle(dictionary, "DoubleLongLeftArrow;", HtmlEntityService.Convert(10232));
			HtmlEntityService.AddSingle(dictionary, "DoubleLongLeftRightArrow;", HtmlEntityService.Convert(10234));
			HtmlEntityService.AddSingle(dictionary, "DoubleLongRightArrow;", HtmlEntityService.Convert(10233));
			HtmlEntityService.AddSingle(dictionary, "DoubleRightArrow;", HtmlEntityService.Convert(8658));
			HtmlEntityService.AddSingle(dictionary, "DoubleRightTee;", HtmlEntityService.Convert(8872));
			HtmlEntityService.AddSingle(dictionary, "DoubleUpArrow;", HtmlEntityService.Convert(8657));
			HtmlEntityService.AddSingle(dictionary, "DoubleUpDownArrow;", HtmlEntityService.Convert(8661));
			HtmlEntityService.AddSingle(dictionary, "DoubleVerticalBar;", HtmlEntityService.Convert(8741));
			HtmlEntityService.AddSingle(dictionary, "DownArrow;", HtmlEntityService.Convert(8595));
			HtmlEntityService.AddSingle(dictionary, "Downarrow;", HtmlEntityService.Convert(8659));
			HtmlEntityService.AddSingle(dictionary, "DownArrowBar;", HtmlEntityService.Convert(10515));
			HtmlEntityService.AddSingle(dictionary, "DownArrowUpArrow;", HtmlEntityService.Convert(8693));
			HtmlEntityService.AddSingle(dictionary, "DownBreve;", HtmlEntityService.Convert(785));
			HtmlEntityService.AddSingle(dictionary, "DownLeftRightVector;", HtmlEntityService.Convert(10576));
			HtmlEntityService.AddSingle(dictionary, "DownLeftTeeVector;", HtmlEntityService.Convert(10590));
			HtmlEntityService.AddSingle(dictionary, "DownLeftVector;", HtmlEntityService.Convert(8637));
			HtmlEntityService.AddSingle(dictionary, "DownLeftVectorBar;", HtmlEntityService.Convert(10582));
			HtmlEntityService.AddSingle(dictionary, "DownRightTeeVector;", HtmlEntityService.Convert(10591));
			HtmlEntityService.AddSingle(dictionary, "DownRightVector;", HtmlEntityService.Convert(8641));
			HtmlEntityService.AddSingle(dictionary, "DownRightVectorBar;", HtmlEntityService.Convert(10583));
			HtmlEntityService.AddSingle(dictionary, "DownTee;", HtmlEntityService.Convert(8868));
			HtmlEntityService.AddSingle(dictionary, "DownTeeArrow;", HtmlEntityService.Convert(8615));
			HtmlEntityService.AddSingle(dictionary, "Dscr;", HtmlEntityService.Convert(119967));
			HtmlEntityService.AddSingle(dictionary, "DScy;", HtmlEntityService.Convert(1029));
			HtmlEntityService.AddSingle(dictionary, "Dstrok;", HtmlEntityService.Convert(272));
			HtmlEntityService.AddSingle(dictionary, "DZcy;", HtmlEntityService.Convert(1039));
			return dictionary;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002325C File Offset: 0x0002145C
		private Dictionary<string, string> GetSymbolLittleE()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "eacute;", HtmlEntityService.Convert(233));
			HtmlEntityService.AddSingle(dictionary, "easter;", HtmlEntityService.Convert(10862));
			HtmlEntityService.AddSingle(dictionary, "ecaron;", HtmlEntityService.Convert(283));
			HtmlEntityService.AddSingle(dictionary, "ecir;", HtmlEntityService.Convert(8790));
			HtmlEntityService.AddBoth(dictionary, "ecirc;", HtmlEntityService.Convert(234));
			HtmlEntityService.AddSingle(dictionary, "ecolon;", HtmlEntityService.Convert(8789));
			HtmlEntityService.AddSingle(dictionary, "ecy;", HtmlEntityService.Convert(1101));
			HtmlEntityService.AddSingle(dictionary, "eDDot;", HtmlEntityService.Convert(10871));
			HtmlEntityService.AddSingle(dictionary, "eDot;", HtmlEntityService.Convert(8785));
			HtmlEntityService.AddSingle(dictionary, "edot;", HtmlEntityService.Convert(279));
			HtmlEntityService.AddSingle(dictionary, "ee;", HtmlEntityService.Convert(8519));
			HtmlEntityService.AddSingle(dictionary, "efDot;", HtmlEntityService.Convert(8786));
			HtmlEntityService.AddSingle(dictionary, "efr;", HtmlEntityService.Convert(120098));
			HtmlEntityService.AddSingle(dictionary, "eg;", HtmlEntityService.Convert(10906));
			HtmlEntityService.AddBoth(dictionary, "egrave;", HtmlEntityService.Convert(232));
			HtmlEntityService.AddSingle(dictionary, "egs;", HtmlEntityService.Convert(10902));
			HtmlEntityService.AddSingle(dictionary, "egsdot;", HtmlEntityService.Convert(10904));
			HtmlEntityService.AddSingle(dictionary, "el;", HtmlEntityService.Convert(10905));
			HtmlEntityService.AddSingle(dictionary, "elinters;", HtmlEntityService.Convert(9191));
			HtmlEntityService.AddSingle(dictionary, "ell;", HtmlEntityService.Convert(8467));
			HtmlEntityService.AddSingle(dictionary, "els;", HtmlEntityService.Convert(10901));
			HtmlEntityService.AddSingle(dictionary, "elsdot;", HtmlEntityService.Convert(10903));
			HtmlEntityService.AddSingle(dictionary, "emacr;", HtmlEntityService.Convert(275));
			HtmlEntityService.AddSingle(dictionary, "empty;", HtmlEntityService.Convert(8709));
			HtmlEntityService.AddSingle(dictionary, "emptyset;", HtmlEntityService.Convert(8709));
			HtmlEntityService.AddSingle(dictionary, "emptyv;", HtmlEntityService.Convert(8709));
			HtmlEntityService.AddSingle(dictionary, "emsp;", HtmlEntityService.Convert(8195));
			HtmlEntityService.AddSingle(dictionary, "emsp13;", HtmlEntityService.Convert(8196));
			HtmlEntityService.AddSingle(dictionary, "emsp14;", HtmlEntityService.Convert(8197));
			HtmlEntityService.AddSingle(dictionary, "eng;", HtmlEntityService.Convert(331));
			HtmlEntityService.AddSingle(dictionary, "ensp;", HtmlEntityService.Convert(8194));
			HtmlEntityService.AddSingle(dictionary, "eogon;", HtmlEntityService.Convert(281));
			HtmlEntityService.AddSingle(dictionary, "eopf;", HtmlEntityService.Convert(120150));
			HtmlEntityService.AddSingle(dictionary, "epar;", HtmlEntityService.Convert(8917));
			HtmlEntityService.AddSingle(dictionary, "eparsl;", HtmlEntityService.Convert(10723));
			HtmlEntityService.AddSingle(dictionary, "eplus;", HtmlEntityService.Convert(10865));
			HtmlEntityService.AddSingle(dictionary, "epsi;", HtmlEntityService.Convert(949));
			HtmlEntityService.AddSingle(dictionary, "epsilon;", HtmlEntityService.Convert(949));
			HtmlEntityService.AddSingle(dictionary, "epsiv;", HtmlEntityService.Convert(1013));
			HtmlEntityService.AddSingle(dictionary, "eqcirc;", HtmlEntityService.Convert(8790));
			HtmlEntityService.AddSingle(dictionary, "eqcolon;", HtmlEntityService.Convert(8789));
			HtmlEntityService.AddSingle(dictionary, "eqsim;", HtmlEntityService.Convert(8770));
			HtmlEntityService.AddSingle(dictionary, "eqslantgtr;", HtmlEntityService.Convert(10902));
			HtmlEntityService.AddSingle(dictionary, "eqslantless;", HtmlEntityService.Convert(10901));
			HtmlEntityService.AddSingle(dictionary, "equals;", HtmlEntityService.Convert(61));
			HtmlEntityService.AddSingle(dictionary, "equest;", HtmlEntityService.Convert(8799));
			HtmlEntityService.AddSingle(dictionary, "equiv;", HtmlEntityService.Convert(8801));
			HtmlEntityService.AddSingle(dictionary, "equivDD;", HtmlEntityService.Convert(10872));
			HtmlEntityService.AddSingle(dictionary, "eqvparsl;", HtmlEntityService.Convert(10725));
			HtmlEntityService.AddSingle(dictionary, "erarr;", HtmlEntityService.Convert(10609));
			HtmlEntityService.AddSingle(dictionary, "erDot;", HtmlEntityService.Convert(8787));
			HtmlEntityService.AddSingle(dictionary, "escr;", HtmlEntityService.Convert(8495));
			HtmlEntityService.AddSingle(dictionary, "esdot;", HtmlEntityService.Convert(8784));
			HtmlEntityService.AddSingle(dictionary, "esim;", HtmlEntityService.Convert(8770));
			HtmlEntityService.AddSingle(dictionary, "eta;", HtmlEntityService.Convert(951));
			HtmlEntityService.AddBoth(dictionary, "eth;", HtmlEntityService.Convert(240));
			HtmlEntityService.AddBoth(dictionary, "euml;", HtmlEntityService.Convert(235));
			HtmlEntityService.AddSingle(dictionary, "euro;", HtmlEntityService.Convert(8364));
			HtmlEntityService.AddSingle(dictionary, "excl;", HtmlEntityService.Convert(33));
			HtmlEntityService.AddSingle(dictionary, "exist;", HtmlEntityService.Convert(8707));
			HtmlEntityService.AddSingle(dictionary, "expectation;", HtmlEntityService.Convert(8496));
			HtmlEntityService.AddSingle(dictionary, "exponentiale;", HtmlEntityService.Convert(8519));
			return dictionary;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00023780 File Offset: 0x00021980
		private Dictionary<string, string> GetSymbolBigE()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "Eacute;", HtmlEntityService.Convert(201));
			HtmlEntityService.AddSingle(dictionary, "Ecaron;", HtmlEntityService.Convert(282));
			HtmlEntityService.AddBoth(dictionary, "Ecirc;", HtmlEntityService.Convert(202));
			HtmlEntityService.AddSingle(dictionary, "Ecy;", HtmlEntityService.Convert(1069));
			HtmlEntityService.AddSingle(dictionary, "Edot;", HtmlEntityService.Convert(278));
			HtmlEntityService.AddSingle(dictionary, "Efr;", HtmlEntityService.Convert(120072));
			HtmlEntityService.AddBoth(dictionary, "Egrave;", HtmlEntityService.Convert(200));
			HtmlEntityService.AddSingle(dictionary, "Element;", HtmlEntityService.Convert(8712));
			HtmlEntityService.AddSingle(dictionary, "Emacr;", HtmlEntityService.Convert(274));
			HtmlEntityService.AddSingle(dictionary, "EmptySmallSquare;", HtmlEntityService.Convert(9723));
			HtmlEntityService.AddSingle(dictionary, "EmptyVerySmallSquare;", HtmlEntityService.Convert(9643));
			HtmlEntityService.AddSingle(dictionary, "ENG;", HtmlEntityService.Convert(330));
			HtmlEntityService.AddSingle(dictionary, "Eogon;", HtmlEntityService.Convert(280));
			HtmlEntityService.AddSingle(dictionary, "Eopf;", HtmlEntityService.Convert(120124));
			HtmlEntityService.AddSingle(dictionary, "Epsilon;", HtmlEntityService.Convert(917));
			HtmlEntityService.AddSingle(dictionary, "Equal;", HtmlEntityService.Convert(10869));
			HtmlEntityService.AddSingle(dictionary, "EqualTilde;", HtmlEntityService.Convert(8770));
			HtmlEntityService.AddSingle(dictionary, "Equilibrium;", HtmlEntityService.Convert(8652));
			HtmlEntityService.AddSingle(dictionary, "Escr;", HtmlEntityService.Convert(8496));
			HtmlEntityService.AddSingle(dictionary, "Esim;", HtmlEntityService.Convert(10867));
			HtmlEntityService.AddSingle(dictionary, "Eta;", HtmlEntityService.Convert(919));
			HtmlEntityService.AddBoth(dictionary, "ETH;", HtmlEntityService.Convert(208));
			HtmlEntityService.AddBoth(dictionary, "Euml;", HtmlEntityService.Convert(203));
			HtmlEntityService.AddSingle(dictionary, "Exists;", HtmlEntityService.Convert(8707));
			HtmlEntityService.AddSingle(dictionary, "ExponentialE;", HtmlEntityService.Convert(8519));
			return dictionary;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000239A0 File Offset: 0x00021BA0
		private Dictionary<string, string> GetSymbolLittleF()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "fallingdotseq;", HtmlEntityService.Convert(8786));
			HtmlEntityService.AddSingle(dictionary, "fcy;", HtmlEntityService.Convert(1092));
			HtmlEntityService.AddSingle(dictionary, "female;", HtmlEntityService.Convert(9792));
			HtmlEntityService.AddSingle(dictionary, "ffilig;", HtmlEntityService.Convert(64259));
			HtmlEntityService.AddSingle(dictionary, "fflig;", HtmlEntityService.Convert(64256));
			HtmlEntityService.AddSingle(dictionary, "ffllig;", HtmlEntityService.Convert(64260));
			HtmlEntityService.AddSingle(dictionary, "ffr;", HtmlEntityService.Convert(120099));
			HtmlEntityService.AddSingle(dictionary, "filig;", HtmlEntityService.Convert(64257));
			HtmlEntityService.AddSingle(dictionary, "fjlig;", HtmlEntityService.Convert(102, 106));
			HtmlEntityService.AddSingle(dictionary, "flat;", HtmlEntityService.Convert(9837));
			HtmlEntityService.AddSingle(dictionary, "fllig;", HtmlEntityService.Convert(64258));
			HtmlEntityService.AddSingle(dictionary, "fltns;", HtmlEntityService.Convert(9649));
			HtmlEntityService.AddSingle(dictionary, "fnof;", HtmlEntityService.Convert(402));
			HtmlEntityService.AddSingle(dictionary, "fopf;", HtmlEntityService.Convert(120151));
			HtmlEntityService.AddSingle(dictionary, "forall;", HtmlEntityService.Convert(8704));
			HtmlEntityService.AddSingle(dictionary, "fork;", HtmlEntityService.Convert(8916));
			HtmlEntityService.AddSingle(dictionary, "forkv;", HtmlEntityService.Convert(10969));
			HtmlEntityService.AddSingle(dictionary, "fpartint;", HtmlEntityService.Convert(10765));
			HtmlEntityService.AddBoth(dictionary, "frac12;", HtmlEntityService.Convert(189));
			HtmlEntityService.AddSingle(dictionary, "frac13;", HtmlEntityService.Convert(8531));
			HtmlEntityService.AddBoth(dictionary, "frac14;", HtmlEntityService.Convert(188));
			HtmlEntityService.AddSingle(dictionary, "frac15;", HtmlEntityService.Convert(8533));
			HtmlEntityService.AddSingle(dictionary, "frac16;", HtmlEntityService.Convert(8537));
			HtmlEntityService.AddSingle(dictionary, "frac18;", HtmlEntityService.Convert(8539));
			HtmlEntityService.AddSingle(dictionary, "frac23;", HtmlEntityService.Convert(8532));
			HtmlEntityService.AddSingle(dictionary, "frac25;", HtmlEntityService.Convert(8534));
			HtmlEntityService.AddBoth(dictionary, "frac34;", HtmlEntityService.Convert(190));
			HtmlEntityService.AddSingle(dictionary, "frac35;", HtmlEntityService.Convert(8535));
			HtmlEntityService.AddSingle(dictionary, "frac38;", HtmlEntityService.Convert(8540));
			HtmlEntityService.AddSingle(dictionary, "frac45;", HtmlEntityService.Convert(8536));
			HtmlEntityService.AddSingle(dictionary, "frac56;", HtmlEntityService.Convert(8538));
			HtmlEntityService.AddSingle(dictionary, "frac58;", HtmlEntityService.Convert(8541));
			HtmlEntityService.AddSingle(dictionary, "frac78;", HtmlEntityService.Convert(8542));
			HtmlEntityService.AddSingle(dictionary, "frasl;", HtmlEntityService.Convert(8260));
			HtmlEntityService.AddSingle(dictionary, "frown;", HtmlEntityService.Convert(8994));
			HtmlEntityService.AddSingle(dictionary, "fscr;", HtmlEntityService.Convert(119995));
			return dictionary;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00023CA8 File Offset: 0x00021EA8
		private Dictionary<string, string> GetSymbolBigF()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Fcy;", HtmlEntityService.Convert(1060));
			HtmlEntityService.AddSingle(dictionary, "Ffr;", HtmlEntityService.Convert(120073));
			HtmlEntityService.AddSingle(dictionary, "FilledSmallSquare;", HtmlEntityService.Convert(9724));
			HtmlEntityService.AddSingle(dictionary, "FilledVerySmallSquare;", HtmlEntityService.Convert(9642));
			HtmlEntityService.AddSingle(dictionary, "Fopf;", HtmlEntityService.Convert(120125));
			HtmlEntityService.AddSingle(dictionary, "ForAll;", HtmlEntityService.Convert(8704));
			HtmlEntityService.AddSingle(dictionary, "Fouriertrf;", HtmlEntityService.Convert(8497));
			HtmlEntityService.AddSingle(dictionary, "Fscr;", HtmlEntityService.Convert(8497));
			return dictionary;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00023D64 File Offset: 0x00021F64
		private Dictionary<string, string> GetSymbolLittleG()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "gacute;", HtmlEntityService.Convert(501));
			HtmlEntityService.AddSingle(dictionary, "gamma;", HtmlEntityService.Convert(947));
			HtmlEntityService.AddSingle(dictionary, "gammad;", HtmlEntityService.Convert(989));
			HtmlEntityService.AddSingle(dictionary, "gap;", HtmlEntityService.Convert(10886));
			HtmlEntityService.AddSingle(dictionary, "gbreve;", HtmlEntityService.Convert(287));
			HtmlEntityService.AddSingle(dictionary, "gcirc;", HtmlEntityService.Convert(285));
			HtmlEntityService.AddSingle(dictionary, "gcy;", HtmlEntityService.Convert(1075));
			HtmlEntityService.AddSingle(dictionary, "gdot;", HtmlEntityService.Convert(289));
			HtmlEntityService.AddSingle(dictionary, "gE;", HtmlEntityService.Convert(8807));
			HtmlEntityService.AddSingle(dictionary, "ge;", HtmlEntityService.Convert(8805));
			HtmlEntityService.AddSingle(dictionary, "gEl;", HtmlEntityService.Convert(10892));
			HtmlEntityService.AddSingle(dictionary, "gel;", HtmlEntityService.Convert(8923));
			HtmlEntityService.AddSingle(dictionary, "geq;", HtmlEntityService.Convert(8805));
			HtmlEntityService.AddSingle(dictionary, "geqq;", HtmlEntityService.Convert(8807));
			HtmlEntityService.AddSingle(dictionary, "geqslant;", HtmlEntityService.Convert(10878));
			HtmlEntityService.AddSingle(dictionary, "ges;", HtmlEntityService.Convert(10878));
			HtmlEntityService.AddSingle(dictionary, "gescc;", HtmlEntityService.Convert(10921));
			HtmlEntityService.AddSingle(dictionary, "gesdot;", HtmlEntityService.Convert(10880));
			HtmlEntityService.AddSingle(dictionary, "gesdoto;", HtmlEntityService.Convert(10882));
			HtmlEntityService.AddSingle(dictionary, "gesdotol;", HtmlEntityService.Convert(10884));
			HtmlEntityService.AddSingle(dictionary, "gesl;", HtmlEntityService.Convert(8923, 65024));
			HtmlEntityService.AddSingle(dictionary, "gesles;", HtmlEntityService.Convert(10900));
			HtmlEntityService.AddSingle(dictionary, "gfr;", HtmlEntityService.Convert(120100));
			HtmlEntityService.AddSingle(dictionary, "gg;", HtmlEntityService.Convert(8811));
			HtmlEntityService.AddSingle(dictionary, "ggg;", HtmlEntityService.Convert(8921));
			HtmlEntityService.AddSingle(dictionary, "gimel;", HtmlEntityService.Convert(8503));
			HtmlEntityService.AddSingle(dictionary, "gjcy;", HtmlEntityService.Convert(1107));
			HtmlEntityService.AddSingle(dictionary, "gl;", HtmlEntityService.Convert(8823));
			HtmlEntityService.AddSingle(dictionary, "gla;", HtmlEntityService.Convert(10917));
			HtmlEntityService.AddSingle(dictionary, "glE;", HtmlEntityService.Convert(10898));
			HtmlEntityService.AddSingle(dictionary, "glj;", HtmlEntityService.Convert(10916));
			HtmlEntityService.AddSingle(dictionary, "gnap;", HtmlEntityService.Convert(10890));
			HtmlEntityService.AddSingle(dictionary, "gnapprox;", HtmlEntityService.Convert(10890));
			HtmlEntityService.AddSingle(dictionary, "gnE;", HtmlEntityService.Convert(8809));
			HtmlEntityService.AddSingle(dictionary, "gne;", HtmlEntityService.Convert(10888));
			HtmlEntityService.AddSingle(dictionary, "gneq;", HtmlEntityService.Convert(10888));
			HtmlEntityService.AddSingle(dictionary, "gneqq;", HtmlEntityService.Convert(8809));
			HtmlEntityService.AddSingle(dictionary, "gnsim;", HtmlEntityService.Convert(8935));
			HtmlEntityService.AddSingle(dictionary, "gopf;", HtmlEntityService.Convert(120152));
			HtmlEntityService.AddSingle(dictionary, "grave;", HtmlEntityService.Convert(96));
			HtmlEntityService.AddSingle(dictionary, "gscr;", HtmlEntityService.Convert(8458));
			HtmlEntityService.AddSingle(dictionary, "gsim;", HtmlEntityService.Convert(8819));
			HtmlEntityService.AddSingle(dictionary, "gsime;", HtmlEntityService.Convert(10894));
			HtmlEntityService.AddSingle(dictionary, "gsiml;", HtmlEntityService.Convert(10896));
			HtmlEntityService.AddBoth(dictionary, "gt;", HtmlEntityService.Convert(62));
			HtmlEntityService.AddSingle(dictionary, "gtcc;", HtmlEntityService.Convert(10919));
			HtmlEntityService.AddSingle(dictionary, "gtcir;", HtmlEntityService.Convert(10874));
			HtmlEntityService.AddSingle(dictionary, "gtdot;", HtmlEntityService.Convert(8919));
			HtmlEntityService.AddSingle(dictionary, "gtlPar;", HtmlEntityService.Convert(10645));
			HtmlEntityService.AddSingle(dictionary, "gtquest;", HtmlEntityService.Convert(10876));
			HtmlEntityService.AddSingle(dictionary, "gtrapprox;", HtmlEntityService.Convert(10886));
			HtmlEntityService.AddSingle(dictionary, "gtrarr;", HtmlEntityService.Convert(10616));
			HtmlEntityService.AddSingle(dictionary, "gtrdot;", HtmlEntityService.Convert(8919));
			HtmlEntityService.AddSingle(dictionary, "gtreqless;", HtmlEntityService.Convert(8923));
			HtmlEntityService.AddSingle(dictionary, "gtreqqless;", HtmlEntityService.Convert(10892));
			HtmlEntityService.AddSingle(dictionary, "gtrless;", HtmlEntityService.Convert(8823));
			HtmlEntityService.AddSingle(dictionary, "gtrsim;", HtmlEntityService.Convert(8819));
			HtmlEntityService.AddSingle(dictionary, "gvertneqq;", HtmlEntityService.Convert(8809, 65024));
			HtmlEntityService.AddSingle(dictionary, "gvnE;", HtmlEntityService.Convert(8809, 65024));
			return dictionary;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00024258 File Offset: 0x00022458
		private Dictionary<string, string> GetSymbolBigG()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Gamma;", HtmlEntityService.Convert(915));
			HtmlEntityService.AddSingle(dictionary, "Gammad;", HtmlEntityService.Convert(988));
			HtmlEntityService.AddSingle(dictionary, "Gbreve;", HtmlEntityService.Convert(286));
			HtmlEntityService.AddSingle(dictionary, "Gcedil;", HtmlEntityService.Convert(290));
			HtmlEntityService.AddSingle(dictionary, "Gcirc;", HtmlEntityService.Convert(284));
			HtmlEntityService.AddSingle(dictionary, "Gcy;", HtmlEntityService.Convert(1043));
			HtmlEntityService.AddSingle(dictionary, "Gdot;", HtmlEntityService.Convert(288));
			HtmlEntityService.AddSingle(dictionary, "Gfr;", HtmlEntityService.Convert(120074));
			HtmlEntityService.AddSingle(dictionary, "Gg;", HtmlEntityService.Convert(8921));
			HtmlEntityService.AddSingle(dictionary, "GJcy;", HtmlEntityService.Convert(1027));
			HtmlEntityService.AddSingle(dictionary, "Gopf;", HtmlEntityService.Convert(120126));
			HtmlEntityService.AddSingle(dictionary, "GreaterEqual;", HtmlEntityService.Convert(8805));
			HtmlEntityService.AddSingle(dictionary, "GreaterEqualLess;", HtmlEntityService.Convert(8923));
			HtmlEntityService.AddSingle(dictionary, "GreaterFullEqual;", HtmlEntityService.Convert(8807));
			HtmlEntityService.AddSingle(dictionary, "GreaterGreater;", HtmlEntityService.Convert(10914));
			HtmlEntityService.AddSingle(dictionary, "GreaterLess;", HtmlEntityService.Convert(8823));
			HtmlEntityService.AddSingle(dictionary, "GreaterSlantEqual;", HtmlEntityService.Convert(10878));
			HtmlEntityService.AddSingle(dictionary, "GreaterTilde;", HtmlEntityService.Convert(8819));
			HtmlEntityService.AddSingle(dictionary, "Gscr;", HtmlEntityService.Convert(119970));
			HtmlEntityService.AddBoth(dictionary, "GT;", HtmlEntityService.Convert(62));
			HtmlEntityService.AddSingle(dictionary, "Gt;", HtmlEntityService.Convert(8811));
			return dictionary;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00024420 File Offset: 0x00022620
		private Dictionary<string, string> GetSymbolLittleH()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "hairsp;", HtmlEntityService.Convert(8202));
			HtmlEntityService.AddSingle(dictionary, "half;", HtmlEntityService.Convert(189));
			HtmlEntityService.AddSingle(dictionary, "hamilt;", HtmlEntityService.Convert(8459));
			HtmlEntityService.AddSingle(dictionary, "hardcy;", HtmlEntityService.Convert(1098));
			HtmlEntityService.AddSingle(dictionary, "hArr;", HtmlEntityService.Convert(8660));
			HtmlEntityService.AddSingle(dictionary, "harr;", HtmlEntityService.Convert(8596));
			HtmlEntityService.AddSingle(dictionary, "harrcir;", HtmlEntityService.Convert(10568));
			HtmlEntityService.AddSingle(dictionary, "harrw;", HtmlEntityService.Convert(8621));
			HtmlEntityService.AddSingle(dictionary, "hbar;", HtmlEntityService.Convert(8463));
			HtmlEntityService.AddSingle(dictionary, "hcirc;", HtmlEntityService.Convert(293));
			HtmlEntityService.AddSingle(dictionary, "hearts;", HtmlEntityService.Convert(9829));
			HtmlEntityService.AddSingle(dictionary, "heartsuit;", HtmlEntityService.Convert(9829));
			HtmlEntityService.AddSingle(dictionary, "hellip;", HtmlEntityService.Convert(8230));
			HtmlEntityService.AddSingle(dictionary, "hercon;", HtmlEntityService.Convert(8889));
			HtmlEntityService.AddSingle(dictionary, "hfr;", HtmlEntityService.Convert(120101));
			HtmlEntityService.AddSingle(dictionary, "hksearow;", HtmlEntityService.Convert(10533));
			HtmlEntityService.AddSingle(dictionary, "hkswarow;", HtmlEntityService.Convert(10534));
			HtmlEntityService.AddSingle(dictionary, "hoarr;", HtmlEntityService.Convert(8703));
			HtmlEntityService.AddSingle(dictionary, "homtht;", HtmlEntityService.Convert(8763));
			HtmlEntityService.AddSingle(dictionary, "hookleftarrow;", HtmlEntityService.Convert(8617));
			HtmlEntityService.AddSingle(dictionary, "hookrightarrow;", HtmlEntityService.Convert(8618));
			HtmlEntityService.AddSingle(dictionary, "hopf;", HtmlEntityService.Convert(120153));
			HtmlEntityService.AddSingle(dictionary, "horbar;", HtmlEntityService.Convert(8213));
			HtmlEntityService.AddSingle(dictionary, "hscr;", HtmlEntityService.Convert(119997));
			HtmlEntityService.AddSingle(dictionary, "hslash;", HtmlEntityService.Convert(8463));
			HtmlEntityService.AddSingle(dictionary, "hstrok;", HtmlEntityService.Convert(295));
			HtmlEntityService.AddSingle(dictionary, "hybull;", HtmlEntityService.Convert(8259));
			HtmlEntityService.AddSingle(dictionary, "hyphen;", HtmlEntityService.Convert(8208));
			return dictionary;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00024680 File Offset: 0x00022880
		private Dictionary<string, string> GetSymbolBigH()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Hacek;", HtmlEntityService.Convert(711));
			HtmlEntityService.AddSingle(dictionary, "HARDcy;", HtmlEntityService.Convert(1066));
			HtmlEntityService.AddSingle(dictionary, "Hat;", HtmlEntityService.Convert(94));
			HtmlEntityService.AddSingle(dictionary, "Hcirc;", HtmlEntityService.Convert(292));
			HtmlEntityService.AddSingle(dictionary, "Hfr;", HtmlEntityService.Convert(8460));
			HtmlEntityService.AddSingle(dictionary, "HilbertSpace;", HtmlEntityService.Convert(8459));
			HtmlEntityService.AddSingle(dictionary, "Hopf;", HtmlEntityService.Convert(8461));
			HtmlEntityService.AddSingle(dictionary, "HorizontalLine;", HtmlEntityService.Convert(9472));
			HtmlEntityService.AddSingle(dictionary, "Hscr;", HtmlEntityService.Convert(8459));
			HtmlEntityService.AddSingle(dictionary, "Hstrok;", HtmlEntityService.Convert(294));
			HtmlEntityService.AddSingle(dictionary, "HumpDownHump;", HtmlEntityService.Convert(8782));
			HtmlEntityService.AddSingle(dictionary, "HumpEqual;", HtmlEntityService.Convert(8783));
			return dictionary;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002478C File Offset: 0x0002298C
		private Dictionary<string, string> GetSymbolLittleI()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "iacute;", HtmlEntityService.Convert(237));
			HtmlEntityService.AddSingle(dictionary, "ic;", HtmlEntityService.Convert(8291));
			HtmlEntityService.AddBoth(dictionary, "icirc;", HtmlEntityService.Convert(238));
			HtmlEntityService.AddSingle(dictionary, "icy;", HtmlEntityService.Convert(1080));
			HtmlEntityService.AddSingle(dictionary, "iecy;", HtmlEntityService.Convert(1077));
			HtmlEntityService.AddBoth(dictionary, "iexcl;", HtmlEntityService.Convert(161));
			HtmlEntityService.AddSingle(dictionary, "iff;", HtmlEntityService.Convert(8660));
			HtmlEntityService.AddSingle(dictionary, "ifr;", HtmlEntityService.Convert(120102));
			HtmlEntityService.AddBoth(dictionary, "igrave;", HtmlEntityService.Convert(236));
			HtmlEntityService.AddSingle(dictionary, "ii;", HtmlEntityService.Convert(8520));
			HtmlEntityService.AddSingle(dictionary, "iiiint;", HtmlEntityService.Convert(10764));
			HtmlEntityService.AddSingle(dictionary, "iiint;", HtmlEntityService.Convert(8749));
			HtmlEntityService.AddSingle(dictionary, "iinfin;", HtmlEntityService.Convert(10716));
			HtmlEntityService.AddSingle(dictionary, "iiota;", HtmlEntityService.Convert(8489));
			HtmlEntityService.AddSingle(dictionary, "ijlig;", HtmlEntityService.Convert(307));
			HtmlEntityService.AddSingle(dictionary, "imacr;", HtmlEntityService.Convert(299));
			HtmlEntityService.AddSingle(dictionary, "image;", HtmlEntityService.Convert(8465));
			HtmlEntityService.AddSingle(dictionary, "imagline;", HtmlEntityService.Convert(8464));
			HtmlEntityService.AddSingle(dictionary, "imagpart;", HtmlEntityService.Convert(8465));
			HtmlEntityService.AddSingle(dictionary, "imath;", HtmlEntityService.Convert(305));
			HtmlEntityService.AddSingle(dictionary, "imof;", HtmlEntityService.Convert(8887));
			HtmlEntityService.AddSingle(dictionary, "imped;", HtmlEntityService.Convert(437));
			HtmlEntityService.AddSingle(dictionary, "in;", HtmlEntityService.Convert(8712));
			HtmlEntityService.AddSingle(dictionary, "incare;", HtmlEntityService.Convert(8453));
			HtmlEntityService.AddSingle(dictionary, "infin;", HtmlEntityService.Convert(8734));
			HtmlEntityService.AddSingle(dictionary, "infintie;", HtmlEntityService.Convert(10717));
			HtmlEntityService.AddSingle(dictionary, "inodot;", HtmlEntityService.Convert(305));
			HtmlEntityService.AddSingle(dictionary, "int;", HtmlEntityService.Convert(8747));
			HtmlEntityService.AddSingle(dictionary, "intcal;", HtmlEntityService.Convert(8890));
			HtmlEntityService.AddSingle(dictionary, "integers;", HtmlEntityService.Convert(8484));
			HtmlEntityService.AddSingle(dictionary, "intercal;", HtmlEntityService.Convert(8890));
			HtmlEntityService.AddSingle(dictionary, "intlarhk;", HtmlEntityService.Convert(10775));
			HtmlEntityService.AddSingle(dictionary, "intprod;", HtmlEntityService.Convert(10812));
			HtmlEntityService.AddSingle(dictionary, "iocy;", HtmlEntityService.Convert(1105));
			HtmlEntityService.AddSingle(dictionary, "iogon;", HtmlEntityService.Convert(303));
			HtmlEntityService.AddSingle(dictionary, "iopf;", HtmlEntityService.Convert(120154));
			HtmlEntityService.AddSingle(dictionary, "iota;", HtmlEntityService.Convert(953));
			HtmlEntityService.AddSingle(dictionary, "iprod;", HtmlEntityService.Convert(10812));
			HtmlEntityService.AddBoth(dictionary, "iquest;", HtmlEntityService.Convert(191));
			HtmlEntityService.AddSingle(dictionary, "iscr;", HtmlEntityService.Convert(119998));
			HtmlEntityService.AddSingle(dictionary, "isin;", HtmlEntityService.Convert(8712));
			HtmlEntityService.AddSingle(dictionary, "isindot;", HtmlEntityService.Convert(8949));
			HtmlEntityService.AddSingle(dictionary, "isinE;", HtmlEntityService.Convert(8953));
			HtmlEntityService.AddSingle(dictionary, "isins;", HtmlEntityService.Convert(8948));
			HtmlEntityService.AddSingle(dictionary, "isinsv;", HtmlEntityService.Convert(8947));
			HtmlEntityService.AddSingle(dictionary, "isinv;", HtmlEntityService.Convert(8712));
			HtmlEntityService.AddSingle(dictionary, "it;", HtmlEntityService.Convert(8290));
			HtmlEntityService.AddSingle(dictionary, "itilde;", HtmlEntityService.Convert(297));
			HtmlEntityService.AddSingle(dictionary, "iukcy;", HtmlEntityService.Convert(1110));
			HtmlEntityService.AddBoth(dictionary, "iuml;", HtmlEntityService.Convert(239));
			return dictionary;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00024BB8 File Offset: 0x00022DB8
		private Dictionary<string, string> GetSymbolBigI()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "Iacute;", HtmlEntityService.Convert(205));
			HtmlEntityService.AddBoth(dictionary, "Icirc;", HtmlEntityService.Convert(206));
			HtmlEntityService.AddSingle(dictionary, "Icy;", HtmlEntityService.Convert(1048));
			HtmlEntityService.AddSingle(dictionary, "Idot;", HtmlEntityService.Convert(304));
			HtmlEntityService.AddSingle(dictionary, "IEcy;", HtmlEntityService.Convert(1045));
			HtmlEntityService.AddSingle(dictionary, "Ifr;", HtmlEntityService.Convert(8465));
			HtmlEntityService.AddBoth(dictionary, "Igrave;", HtmlEntityService.Convert(204));
			HtmlEntityService.AddSingle(dictionary, "IJlig;", HtmlEntityService.Convert(306));
			HtmlEntityService.AddSingle(dictionary, "Im;", HtmlEntityService.Convert(8465));
			HtmlEntityService.AddSingle(dictionary, "Imacr;", HtmlEntityService.Convert(298));
			HtmlEntityService.AddSingle(dictionary, "ImaginaryI;", HtmlEntityService.Convert(8520));
			HtmlEntityService.AddSingle(dictionary, "Implies;", HtmlEntityService.Convert(8658));
			HtmlEntityService.AddSingle(dictionary, "Int;", HtmlEntityService.Convert(8748));
			HtmlEntityService.AddSingle(dictionary, "Integral;", HtmlEntityService.Convert(8747));
			HtmlEntityService.AddSingle(dictionary, "Intersection;", HtmlEntityService.Convert(8898));
			HtmlEntityService.AddSingle(dictionary, "InvisibleComma;", HtmlEntityService.Convert(8291));
			HtmlEntityService.AddSingle(dictionary, "InvisibleTimes;", HtmlEntityService.Convert(8290));
			HtmlEntityService.AddSingle(dictionary, "IOcy;", HtmlEntityService.Convert(1025));
			HtmlEntityService.AddSingle(dictionary, "Iogon;", HtmlEntityService.Convert(302));
			HtmlEntityService.AddSingle(dictionary, "Iopf;", HtmlEntityService.Convert(120128));
			HtmlEntityService.AddSingle(dictionary, "Iota;", HtmlEntityService.Convert(921));
			HtmlEntityService.AddSingle(dictionary, "Iscr;", HtmlEntityService.Convert(8464));
			HtmlEntityService.AddSingle(dictionary, "Itilde;", HtmlEntityService.Convert(296));
			HtmlEntityService.AddSingle(dictionary, "Iukcy;", HtmlEntityService.Convert(1030));
			HtmlEntityService.AddBoth(dictionary, "Iuml;", HtmlEntityService.Convert(207));
			return dictionary;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00024DD8 File Offset: 0x00022FD8
		private Dictionary<string, string> GetSymbolLittleJ()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "jcirc;", HtmlEntityService.Convert(309));
			HtmlEntityService.AddSingle(dictionary, "jcy;", HtmlEntityService.Convert(1081));
			HtmlEntityService.AddSingle(dictionary, "jfr;", HtmlEntityService.Convert(120103));
			HtmlEntityService.AddSingle(dictionary, "jmath;", HtmlEntityService.Convert(567));
			HtmlEntityService.AddSingle(dictionary, "jopf;", HtmlEntityService.Convert(120155));
			HtmlEntityService.AddSingle(dictionary, "jscr;", HtmlEntityService.Convert(119999));
			HtmlEntityService.AddSingle(dictionary, "jsercy;", HtmlEntityService.Convert(1112));
			HtmlEntityService.AddSingle(dictionary, "jukcy;", HtmlEntityService.Convert(1108));
			return dictionary;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00024E94 File Offset: 0x00023094
		private Dictionary<string, string> GetSymbolBigJ()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Jcirc;", HtmlEntityService.Convert(308));
			HtmlEntityService.AddSingle(dictionary, "Jcy;", HtmlEntityService.Convert(1049));
			HtmlEntityService.AddSingle(dictionary, "Jfr;", HtmlEntityService.Convert(120077));
			HtmlEntityService.AddSingle(dictionary, "Jopf;", HtmlEntityService.Convert(120129));
			HtmlEntityService.AddSingle(dictionary, "Jscr;", HtmlEntityService.Convert(119973));
			HtmlEntityService.AddSingle(dictionary, "Jsercy;", HtmlEntityService.Convert(1032));
			HtmlEntityService.AddSingle(dictionary, "Jukcy;", HtmlEntityService.Convert(1028));
			return dictionary;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00024F3C File Offset: 0x0002313C
		private Dictionary<string, string> GetSymbolLittleK()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "kappa;", HtmlEntityService.Convert(954));
			HtmlEntityService.AddSingle(dictionary, "kappav;", HtmlEntityService.Convert(1008));
			HtmlEntityService.AddSingle(dictionary, "kcedil;", HtmlEntityService.Convert(311));
			HtmlEntityService.AddSingle(dictionary, "kcy;", HtmlEntityService.Convert(1082));
			HtmlEntityService.AddSingle(dictionary, "kfr;", HtmlEntityService.Convert(120104));
			HtmlEntityService.AddSingle(dictionary, "kgreen;", HtmlEntityService.Convert(312));
			HtmlEntityService.AddSingle(dictionary, "khcy;", HtmlEntityService.Convert(1093));
			HtmlEntityService.AddSingle(dictionary, "kjcy;", HtmlEntityService.Convert(1116));
			HtmlEntityService.AddSingle(dictionary, "kopf;", HtmlEntityService.Convert(120156));
			HtmlEntityService.AddSingle(dictionary, "kscr;", HtmlEntityService.Convert(120000));
			return dictionary;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00025020 File Offset: 0x00023220
		private Dictionary<string, string> GetSymbolBigK()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Kappa;", HtmlEntityService.Convert(922));
			HtmlEntityService.AddSingle(dictionary, "Kcedil;", HtmlEntityService.Convert(310));
			HtmlEntityService.AddSingle(dictionary, "Kcy;", HtmlEntityService.Convert(1050));
			HtmlEntityService.AddSingle(dictionary, "Kfr;", HtmlEntityService.Convert(120078));
			HtmlEntityService.AddSingle(dictionary, "KHcy;", HtmlEntityService.Convert(1061));
			HtmlEntityService.AddSingle(dictionary, "KJcy;", HtmlEntityService.Convert(1036));
			HtmlEntityService.AddSingle(dictionary, "Kopf;", HtmlEntityService.Convert(120130));
			HtmlEntityService.AddSingle(dictionary, "Kscr;", HtmlEntityService.Convert(119974));
			return dictionary;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000250DC File Offset: 0x000232DC
		private Dictionary<string, string> GetSymbolLittleL()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "lAarr;", HtmlEntityService.Convert(8666));
			HtmlEntityService.AddSingle(dictionary, "lacute;", HtmlEntityService.Convert(314));
			HtmlEntityService.AddSingle(dictionary, "laemptyv;", HtmlEntityService.Convert(10676));
			HtmlEntityService.AddSingle(dictionary, "lagran;", HtmlEntityService.Convert(8466));
			HtmlEntityService.AddSingle(dictionary, "lambda;", HtmlEntityService.Convert(955));
			HtmlEntityService.AddSingle(dictionary, "lang;", HtmlEntityService.Convert(10216));
			HtmlEntityService.AddSingle(dictionary, "langd;", HtmlEntityService.Convert(10641));
			HtmlEntityService.AddSingle(dictionary, "langle;", HtmlEntityService.Convert(10216));
			HtmlEntityService.AddSingle(dictionary, "lap;", HtmlEntityService.Convert(10885));
			HtmlEntityService.AddBoth(dictionary, "laquo;", HtmlEntityService.Convert(171));
			HtmlEntityService.AddSingle(dictionary, "lArr;", HtmlEntityService.Convert(8656));
			HtmlEntityService.AddSingle(dictionary, "larr;", HtmlEntityService.Convert(8592));
			HtmlEntityService.AddSingle(dictionary, "larrb;", HtmlEntityService.Convert(8676));
			HtmlEntityService.AddSingle(dictionary, "larrbfs;", HtmlEntityService.Convert(10527));
			HtmlEntityService.AddSingle(dictionary, "larrfs;", HtmlEntityService.Convert(10525));
			HtmlEntityService.AddSingle(dictionary, "larrhk;", HtmlEntityService.Convert(8617));
			HtmlEntityService.AddSingle(dictionary, "larrlp;", HtmlEntityService.Convert(8619));
			HtmlEntityService.AddSingle(dictionary, "larrpl;", HtmlEntityService.Convert(10553));
			HtmlEntityService.AddSingle(dictionary, "larrsim;", HtmlEntityService.Convert(10611));
			HtmlEntityService.AddSingle(dictionary, "larrtl;", HtmlEntityService.Convert(8610));
			HtmlEntityService.AddSingle(dictionary, "lat;", HtmlEntityService.Convert(10923));
			HtmlEntityService.AddSingle(dictionary, "lAtail;", HtmlEntityService.Convert(10523));
			HtmlEntityService.AddSingle(dictionary, "latail;", HtmlEntityService.Convert(10521));
			HtmlEntityService.AddSingle(dictionary, "late;", HtmlEntityService.Convert(10925));
			HtmlEntityService.AddSingle(dictionary, "lates;", HtmlEntityService.Convert(10925, 65024));
			HtmlEntityService.AddSingle(dictionary, "lBarr;", HtmlEntityService.Convert(10510));
			HtmlEntityService.AddSingle(dictionary, "lbarr;", HtmlEntityService.Convert(10508));
			HtmlEntityService.AddSingle(dictionary, "lbbrk;", HtmlEntityService.Convert(10098));
			HtmlEntityService.AddSingle(dictionary, "lbrace;", HtmlEntityService.Convert(123));
			HtmlEntityService.AddSingle(dictionary, "lbrack;", HtmlEntityService.Convert(91));
			HtmlEntityService.AddSingle(dictionary, "lbrke;", HtmlEntityService.Convert(10635));
			HtmlEntityService.AddSingle(dictionary, "lbrksld;", HtmlEntityService.Convert(10639));
			HtmlEntityService.AddSingle(dictionary, "lbrkslu;", HtmlEntityService.Convert(10637));
			HtmlEntityService.AddSingle(dictionary, "lcaron;", HtmlEntityService.Convert(318));
			HtmlEntityService.AddSingle(dictionary, "lcedil;", HtmlEntityService.Convert(316));
			HtmlEntityService.AddSingle(dictionary, "lceil;", HtmlEntityService.Convert(8968));
			HtmlEntityService.AddSingle(dictionary, "lcub;", HtmlEntityService.Convert(123));
			HtmlEntityService.AddSingle(dictionary, "lcy;", HtmlEntityService.Convert(1083));
			HtmlEntityService.AddSingle(dictionary, "ldca;", HtmlEntityService.Convert(10550));
			HtmlEntityService.AddSingle(dictionary, "ldquo;", HtmlEntityService.Convert(8220));
			HtmlEntityService.AddSingle(dictionary, "ldquor;", HtmlEntityService.Convert(8222));
			HtmlEntityService.AddSingle(dictionary, "ldrdhar;", HtmlEntityService.Convert(10599));
			HtmlEntityService.AddSingle(dictionary, "ldrushar;", HtmlEntityService.Convert(10571));
			HtmlEntityService.AddSingle(dictionary, "ldsh;", HtmlEntityService.Convert(8626));
			HtmlEntityService.AddSingle(dictionary, "lE;", HtmlEntityService.Convert(8806));
			HtmlEntityService.AddSingle(dictionary, "le;", HtmlEntityService.Convert(8804));
			HtmlEntityService.AddSingle(dictionary, "leftarrow;", HtmlEntityService.Convert(8592));
			HtmlEntityService.AddSingle(dictionary, "leftarrowtail;", HtmlEntityService.Convert(8610));
			HtmlEntityService.AddSingle(dictionary, "leftharpoondown;", HtmlEntityService.Convert(8637));
			HtmlEntityService.AddSingle(dictionary, "leftharpoonup;", HtmlEntityService.Convert(8636));
			HtmlEntityService.AddSingle(dictionary, "leftleftarrows;", HtmlEntityService.Convert(8647));
			HtmlEntityService.AddSingle(dictionary, "leftrightarrow;", HtmlEntityService.Convert(8596));
			HtmlEntityService.AddSingle(dictionary, "leftrightarrows;", HtmlEntityService.Convert(8646));
			HtmlEntityService.AddSingle(dictionary, "leftrightharpoons;", HtmlEntityService.Convert(8651));
			HtmlEntityService.AddSingle(dictionary, "leftrightsquigarrow;", HtmlEntityService.Convert(8621));
			HtmlEntityService.AddSingle(dictionary, "leftthreetimes;", HtmlEntityService.Convert(8907));
			HtmlEntityService.AddSingle(dictionary, "lEg;", HtmlEntityService.Convert(10891));
			HtmlEntityService.AddSingle(dictionary, "leg;", HtmlEntityService.Convert(8922));
			HtmlEntityService.AddSingle(dictionary, "leq;", HtmlEntityService.Convert(8804));
			HtmlEntityService.AddSingle(dictionary, "leqq;", HtmlEntityService.Convert(8806));
			HtmlEntityService.AddSingle(dictionary, "leqslant;", HtmlEntityService.Convert(10877));
			HtmlEntityService.AddSingle(dictionary, "les;", HtmlEntityService.Convert(10877));
			HtmlEntityService.AddSingle(dictionary, "lescc;", HtmlEntityService.Convert(10920));
			HtmlEntityService.AddSingle(dictionary, "lesdot;", HtmlEntityService.Convert(10879));
			HtmlEntityService.AddSingle(dictionary, "lesdoto;", HtmlEntityService.Convert(10881));
			HtmlEntityService.AddSingle(dictionary, "lesdotor;", HtmlEntityService.Convert(10883));
			HtmlEntityService.AddSingle(dictionary, "lesg;", HtmlEntityService.Convert(8922, 65024));
			HtmlEntityService.AddSingle(dictionary, "lesges;", HtmlEntityService.Convert(10899));
			HtmlEntityService.AddSingle(dictionary, "lessapprox;", HtmlEntityService.Convert(10885));
			HtmlEntityService.AddSingle(dictionary, "lessdot;", HtmlEntityService.Convert(8918));
			HtmlEntityService.AddSingle(dictionary, "lesseqgtr;", HtmlEntityService.Convert(8922));
			HtmlEntityService.AddSingle(dictionary, "lesseqqgtr;", HtmlEntityService.Convert(10891));
			HtmlEntityService.AddSingle(dictionary, "lessgtr;", HtmlEntityService.Convert(8822));
			HtmlEntityService.AddSingle(dictionary, "lesssim;", HtmlEntityService.Convert(8818));
			HtmlEntityService.AddSingle(dictionary, "lfisht;", HtmlEntityService.Convert(10620));
			HtmlEntityService.AddSingle(dictionary, "lfloor;", HtmlEntityService.Convert(8970));
			HtmlEntityService.AddSingle(dictionary, "lfr;", HtmlEntityService.Convert(120105));
			HtmlEntityService.AddSingle(dictionary, "lg;", HtmlEntityService.Convert(8822));
			HtmlEntityService.AddSingle(dictionary, "lgE;", HtmlEntityService.Convert(10897));
			HtmlEntityService.AddSingle(dictionary, "lHar;", HtmlEntityService.Convert(10594));
			HtmlEntityService.AddSingle(dictionary, "lhard;", HtmlEntityService.Convert(8637));
			HtmlEntityService.AddSingle(dictionary, "lharu;", HtmlEntityService.Convert(8636));
			HtmlEntityService.AddSingle(dictionary, "lharul;", HtmlEntityService.Convert(10602));
			HtmlEntityService.AddSingle(dictionary, "lhblk;", HtmlEntityService.Convert(9604));
			HtmlEntityService.AddSingle(dictionary, "ljcy;", HtmlEntityService.Convert(1113));
			HtmlEntityService.AddSingle(dictionary, "ll;", HtmlEntityService.Convert(8810));
			HtmlEntityService.AddSingle(dictionary, "llarr;", HtmlEntityService.Convert(8647));
			HtmlEntityService.AddSingle(dictionary, "llcorner;", HtmlEntityService.Convert(8990));
			HtmlEntityService.AddSingle(dictionary, "llhard;", HtmlEntityService.Convert(10603));
			HtmlEntityService.AddSingle(dictionary, "lltri;", HtmlEntityService.Convert(9722));
			HtmlEntityService.AddSingle(dictionary, "lmidot;", HtmlEntityService.Convert(320));
			HtmlEntityService.AddSingle(dictionary, "lmoust;", HtmlEntityService.Convert(9136));
			HtmlEntityService.AddSingle(dictionary, "lmoustache;", HtmlEntityService.Convert(9136));
			HtmlEntityService.AddSingle(dictionary, "lnap;", HtmlEntityService.Convert(10889));
			HtmlEntityService.AddSingle(dictionary, "lnapprox;", HtmlEntityService.Convert(10889));
			HtmlEntityService.AddSingle(dictionary, "lnE;", HtmlEntityService.Convert(8808));
			HtmlEntityService.AddSingle(dictionary, "lne;", HtmlEntityService.Convert(10887));
			HtmlEntityService.AddSingle(dictionary, "lneq;", HtmlEntityService.Convert(10887));
			HtmlEntityService.AddSingle(dictionary, "lneqq;", HtmlEntityService.Convert(8808));
			HtmlEntityService.AddSingle(dictionary, "lnsim;", HtmlEntityService.Convert(8934));
			HtmlEntityService.AddSingle(dictionary, "loang;", HtmlEntityService.Convert(10220));
			HtmlEntityService.AddSingle(dictionary, "loarr;", HtmlEntityService.Convert(8701));
			HtmlEntityService.AddSingle(dictionary, "lobrk;", HtmlEntityService.Convert(10214));
			HtmlEntityService.AddSingle(dictionary, "longleftarrow;", HtmlEntityService.Convert(10229));
			HtmlEntityService.AddSingle(dictionary, "longleftrightarrow;", HtmlEntityService.Convert(10231));
			HtmlEntityService.AddSingle(dictionary, "longmapsto;", HtmlEntityService.Convert(10236));
			HtmlEntityService.AddSingle(dictionary, "longrightarrow;", HtmlEntityService.Convert(10230));
			HtmlEntityService.AddSingle(dictionary, "looparrowleft;", HtmlEntityService.Convert(8619));
			HtmlEntityService.AddSingle(dictionary, "looparrowright;", HtmlEntityService.Convert(8620));
			HtmlEntityService.AddSingle(dictionary, "lopar;", HtmlEntityService.Convert(10629));
			HtmlEntityService.AddSingle(dictionary, "lopf;", HtmlEntityService.Convert(120157));
			HtmlEntityService.AddSingle(dictionary, "loplus;", HtmlEntityService.Convert(10797));
			HtmlEntityService.AddSingle(dictionary, "lotimes;", HtmlEntityService.Convert(10804));
			HtmlEntityService.AddSingle(dictionary, "lowast;", HtmlEntityService.Convert(8727));
			HtmlEntityService.AddSingle(dictionary, "lowbar;", HtmlEntityService.Convert(95));
			HtmlEntityService.AddSingle(dictionary, "loz;", HtmlEntityService.Convert(9674));
			HtmlEntityService.AddSingle(dictionary, "lozenge;", HtmlEntityService.Convert(9674));
			HtmlEntityService.AddSingle(dictionary, "lozf;", HtmlEntityService.Convert(10731));
			HtmlEntityService.AddSingle(dictionary, "lpar;", HtmlEntityService.Convert(40));
			HtmlEntityService.AddSingle(dictionary, "lparlt;", HtmlEntityService.Convert(10643));
			HtmlEntityService.AddSingle(dictionary, "lrarr;", HtmlEntityService.Convert(8646));
			HtmlEntityService.AddSingle(dictionary, "lrcorner;", HtmlEntityService.Convert(8991));
			HtmlEntityService.AddSingle(dictionary, "lrhar;", HtmlEntityService.Convert(8651));
			HtmlEntityService.AddSingle(dictionary, "lrhard;", HtmlEntityService.Convert(10605));
			HtmlEntityService.AddSingle(dictionary, "lrm;", HtmlEntityService.Convert(8206));
			HtmlEntityService.AddSingle(dictionary, "lrtri;", HtmlEntityService.Convert(8895));
			HtmlEntityService.AddSingle(dictionary, "lsaquo;", HtmlEntityService.Convert(8249));
			HtmlEntityService.AddSingle(dictionary, "lscr;", HtmlEntityService.Convert(120001));
			HtmlEntityService.AddSingle(dictionary, "lsh;", HtmlEntityService.Convert(8624));
			HtmlEntityService.AddSingle(dictionary, "lsim;", HtmlEntityService.Convert(8818));
			HtmlEntityService.AddSingle(dictionary, "lsime;", HtmlEntityService.Convert(10893));
			HtmlEntityService.AddSingle(dictionary, "lsimg;", HtmlEntityService.Convert(10895));
			HtmlEntityService.AddSingle(dictionary, "lsqb;", HtmlEntityService.Convert(91));
			HtmlEntityService.AddSingle(dictionary, "lsquo;", HtmlEntityService.Convert(8216));
			HtmlEntityService.AddSingle(dictionary, "lsquor;", HtmlEntityService.Convert(8218));
			HtmlEntityService.AddSingle(dictionary, "lstrok;", HtmlEntityService.Convert(322));
			HtmlEntityService.AddBoth(dictionary, "lt;", HtmlEntityService.Convert(60));
			HtmlEntityService.AddSingle(dictionary, "ltcc;", HtmlEntityService.Convert(10918));
			HtmlEntityService.AddSingle(dictionary, "ltcir;", HtmlEntityService.Convert(10873));
			HtmlEntityService.AddSingle(dictionary, "ltdot;", HtmlEntityService.Convert(8918));
			HtmlEntityService.AddSingle(dictionary, "lthree;", HtmlEntityService.Convert(8907));
			HtmlEntityService.AddSingle(dictionary, "ltimes;", HtmlEntityService.Convert(8905));
			HtmlEntityService.AddSingle(dictionary, "ltlarr;", HtmlEntityService.Convert(10614));
			HtmlEntityService.AddSingle(dictionary, "ltquest;", HtmlEntityService.Convert(10875));
			HtmlEntityService.AddSingle(dictionary, "ltri;", HtmlEntityService.Convert(9667));
			HtmlEntityService.AddSingle(dictionary, "ltrie;", HtmlEntityService.Convert(8884));
			HtmlEntityService.AddSingle(dictionary, "ltrif;", HtmlEntityService.Convert(9666));
			HtmlEntityService.AddSingle(dictionary, "ltrPar;", HtmlEntityService.Convert(10646));
			HtmlEntityService.AddSingle(dictionary, "lurdshar;", HtmlEntityService.Convert(10570));
			HtmlEntityService.AddSingle(dictionary, "luruhar;", HtmlEntityService.Convert(10598));
			HtmlEntityService.AddSingle(dictionary, "lvertneqq;", HtmlEntityService.Convert(8808, 65024));
			HtmlEntityService.AddSingle(dictionary, "lvnE;", HtmlEntityService.Convert(8808, 65024));
			return dictionary;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00025D68 File Offset: 0x00023F68
		private Dictionary<string, string> GetSymbolBigL()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Lacute;", HtmlEntityService.Convert(313));
			HtmlEntityService.AddSingle(dictionary, "Lambda;", HtmlEntityService.Convert(923));
			HtmlEntityService.AddSingle(dictionary, "Lang;", HtmlEntityService.Convert(10218));
			HtmlEntityService.AddSingle(dictionary, "Laplacetrf;", HtmlEntityService.Convert(8466));
			HtmlEntityService.AddSingle(dictionary, "Larr;", HtmlEntityService.Convert(8606));
			HtmlEntityService.AddSingle(dictionary, "Lcaron;", HtmlEntityService.Convert(317));
			HtmlEntityService.AddSingle(dictionary, "Lcedil;", HtmlEntityService.Convert(315));
			HtmlEntityService.AddSingle(dictionary, "Lcy;", HtmlEntityService.Convert(1051));
			HtmlEntityService.AddSingle(dictionary, "LeftAngleBracket;", HtmlEntityService.Convert(10216));
			HtmlEntityService.AddSingle(dictionary, "LeftArrow;", HtmlEntityService.Convert(8592));
			HtmlEntityService.AddSingle(dictionary, "Leftarrow;", HtmlEntityService.Convert(8656));
			HtmlEntityService.AddSingle(dictionary, "LeftArrowBar;", HtmlEntityService.Convert(8676));
			HtmlEntityService.AddSingle(dictionary, "LeftArrowRightArrow;", HtmlEntityService.Convert(8646));
			HtmlEntityService.AddSingle(dictionary, "LeftCeiling;", HtmlEntityService.Convert(8968));
			HtmlEntityService.AddSingle(dictionary, "LeftDoubleBracket;", HtmlEntityService.Convert(10214));
			HtmlEntityService.AddSingle(dictionary, "LeftDownTeeVector;", HtmlEntityService.Convert(10593));
			HtmlEntityService.AddSingle(dictionary, "LeftDownVector;", HtmlEntityService.Convert(8643));
			HtmlEntityService.AddSingle(dictionary, "LeftDownVectorBar;", HtmlEntityService.Convert(10585));
			HtmlEntityService.AddSingle(dictionary, "LeftFloor;", HtmlEntityService.Convert(8970));
			HtmlEntityService.AddSingle(dictionary, "LeftRightArrow;", HtmlEntityService.Convert(8596));
			HtmlEntityService.AddSingle(dictionary, "Leftrightarrow;", HtmlEntityService.Convert(8660));
			HtmlEntityService.AddSingle(dictionary, "LeftRightVector;", HtmlEntityService.Convert(10574));
			HtmlEntityService.AddSingle(dictionary, "LeftTee;", HtmlEntityService.Convert(8867));
			HtmlEntityService.AddSingle(dictionary, "LeftTeeArrow;", HtmlEntityService.Convert(8612));
			HtmlEntityService.AddSingle(dictionary, "LeftTeeVector;", HtmlEntityService.Convert(10586));
			HtmlEntityService.AddSingle(dictionary, "LeftTriangle;", HtmlEntityService.Convert(8882));
			HtmlEntityService.AddSingle(dictionary, "LeftTriangleBar;", HtmlEntityService.Convert(10703));
			HtmlEntityService.AddSingle(dictionary, "LeftTriangleEqual;", HtmlEntityService.Convert(8884));
			HtmlEntityService.AddSingle(dictionary, "LeftUpDownVector;", HtmlEntityService.Convert(10577));
			HtmlEntityService.AddSingle(dictionary, "LeftUpTeeVector;", HtmlEntityService.Convert(10592));
			HtmlEntityService.AddSingle(dictionary, "LeftUpVector;", HtmlEntityService.Convert(8639));
			HtmlEntityService.AddSingle(dictionary, "LeftUpVectorBar;", HtmlEntityService.Convert(10584));
			HtmlEntityService.AddSingle(dictionary, "LeftVector;", HtmlEntityService.Convert(8636));
			HtmlEntityService.AddSingle(dictionary, "LeftVectorBar;", HtmlEntityService.Convert(10578));
			HtmlEntityService.AddSingle(dictionary, "LessEqualGreater;", HtmlEntityService.Convert(8922));
			HtmlEntityService.AddSingle(dictionary, "LessFullEqual;", HtmlEntityService.Convert(8806));
			HtmlEntityService.AddSingle(dictionary, "LessGreater;", HtmlEntityService.Convert(8822));
			HtmlEntityService.AddSingle(dictionary, "LessLess;", HtmlEntityService.Convert(10913));
			HtmlEntityService.AddSingle(dictionary, "LessSlantEqual;", HtmlEntityService.Convert(10877));
			HtmlEntityService.AddSingle(dictionary, "LessTilde;", HtmlEntityService.Convert(8818));
			HtmlEntityService.AddSingle(dictionary, "Lfr;", HtmlEntityService.Convert(120079));
			HtmlEntityService.AddSingle(dictionary, "LJcy;", HtmlEntityService.Convert(1033));
			HtmlEntityService.AddSingle(dictionary, "Ll;", HtmlEntityService.Convert(8920));
			HtmlEntityService.AddSingle(dictionary, "Lleftarrow;", HtmlEntityService.Convert(8666));
			HtmlEntityService.AddSingle(dictionary, "Lmidot;", HtmlEntityService.Convert(319));
			HtmlEntityService.AddSingle(dictionary, "LongLeftArrow;", HtmlEntityService.Convert(10229));
			HtmlEntityService.AddSingle(dictionary, "Longleftarrow;", HtmlEntityService.Convert(10232));
			HtmlEntityService.AddSingle(dictionary, "LongLeftRightArrow;", HtmlEntityService.Convert(10231));
			HtmlEntityService.AddSingle(dictionary, "Longleftrightarrow;", HtmlEntityService.Convert(10234));
			HtmlEntityService.AddSingle(dictionary, "LongRightArrow;", HtmlEntityService.Convert(10230));
			HtmlEntityService.AddSingle(dictionary, "Longrightarrow;", HtmlEntityService.Convert(10233));
			HtmlEntityService.AddSingle(dictionary, "Lopf;", HtmlEntityService.Convert(120131));
			HtmlEntityService.AddSingle(dictionary, "LowerLeftArrow;", HtmlEntityService.Convert(8601));
			HtmlEntityService.AddSingle(dictionary, "LowerRightArrow;", HtmlEntityService.Convert(8600));
			HtmlEntityService.AddSingle(dictionary, "Lscr;", HtmlEntityService.Convert(8466));
			HtmlEntityService.AddSingle(dictionary, "Lsh;", HtmlEntityService.Convert(8624));
			HtmlEntityService.AddSingle(dictionary, "Lstrok;", HtmlEntityService.Convert(321));
			HtmlEntityService.AddBoth(dictionary, "LT;", HtmlEntityService.Convert(60));
			HtmlEntityService.AddSingle(dictionary, "Lt;", HtmlEntityService.Convert(8810));
			return dictionary;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00026250 File Offset: 0x00024450
		private Dictionary<string, string> GetSymbolLittleM()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "macr;", HtmlEntityService.Convert(175));
			HtmlEntityService.AddSingle(dictionary, "male;", HtmlEntityService.Convert(9794));
			HtmlEntityService.AddSingle(dictionary, "malt;", HtmlEntityService.Convert(10016));
			HtmlEntityService.AddSingle(dictionary, "maltese;", HtmlEntityService.Convert(10016));
			HtmlEntityService.AddSingle(dictionary, "map;", HtmlEntityService.Convert(8614));
			HtmlEntityService.AddSingle(dictionary, "mapsto;", HtmlEntityService.Convert(8614));
			HtmlEntityService.AddSingle(dictionary, "mapstodown;", HtmlEntityService.Convert(8615));
			HtmlEntityService.AddSingle(dictionary, "mapstoleft;", HtmlEntityService.Convert(8612));
			HtmlEntityService.AddSingle(dictionary, "mapstoup;", HtmlEntityService.Convert(8613));
			HtmlEntityService.AddSingle(dictionary, "marker;", HtmlEntityService.Convert(9646));
			HtmlEntityService.AddSingle(dictionary, "mcomma;", HtmlEntityService.Convert(10793));
			HtmlEntityService.AddSingle(dictionary, "mcy;", HtmlEntityService.Convert(1084));
			HtmlEntityService.AddSingle(dictionary, "mdash;", HtmlEntityService.Convert(8212));
			HtmlEntityService.AddSingle(dictionary, "mDDot;", HtmlEntityService.Convert(8762));
			HtmlEntityService.AddSingle(dictionary, "measuredangle;", HtmlEntityService.Convert(8737));
			HtmlEntityService.AddSingle(dictionary, "mfr;", HtmlEntityService.Convert(120106));
			HtmlEntityService.AddSingle(dictionary, "mho;", HtmlEntityService.Convert(8487));
			HtmlEntityService.AddBoth(dictionary, "micro;", HtmlEntityService.Convert(181));
			HtmlEntityService.AddSingle(dictionary, "mid;", HtmlEntityService.Convert(8739));
			HtmlEntityService.AddSingle(dictionary, "midast;", HtmlEntityService.Convert(42));
			HtmlEntityService.AddSingle(dictionary, "midcir;", HtmlEntityService.Convert(10992));
			HtmlEntityService.AddBoth(dictionary, "middot;", HtmlEntityService.Convert(183));
			HtmlEntityService.AddSingle(dictionary, "minus;", HtmlEntityService.Convert(8722));
			HtmlEntityService.AddSingle(dictionary, "minusb;", HtmlEntityService.Convert(8863));
			HtmlEntityService.AddSingle(dictionary, "minusd;", HtmlEntityService.Convert(8760));
			HtmlEntityService.AddSingle(dictionary, "minusdu;", HtmlEntityService.Convert(10794));
			HtmlEntityService.AddSingle(dictionary, "mlcp;", HtmlEntityService.Convert(10971));
			HtmlEntityService.AddSingle(dictionary, "mldr;", HtmlEntityService.Convert(8230));
			HtmlEntityService.AddSingle(dictionary, "mnplus;", HtmlEntityService.Convert(8723));
			HtmlEntityService.AddSingle(dictionary, "models;", HtmlEntityService.Convert(8871));
			HtmlEntityService.AddSingle(dictionary, "mopf;", HtmlEntityService.Convert(120158));
			HtmlEntityService.AddSingle(dictionary, "mp;", HtmlEntityService.Convert(8723));
			HtmlEntityService.AddSingle(dictionary, "mscr;", HtmlEntityService.Convert(120002));
			HtmlEntityService.AddSingle(dictionary, "mstpos;", HtmlEntityService.Convert(8766));
			HtmlEntityService.AddSingle(dictionary, "mu;", HtmlEntityService.Convert(956));
			HtmlEntityService.AddSingle(dictionary, "multimap;", HtmlEntityService.Convert(8888));
			HtmlEntityService.AddSingle(dictionary, "mumap;", HtmlEntityService.Convert(8888));
			return dictionary;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00026568 File Offset: 0x00024768
		private Dictionary<string, string> GetSymbolBigM()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Map;", HtmlEntityService.Convert(10501));
			HtmlEntityService.AddSingle(dictionary, "Mcy;", HtmlEntityService.Convert(1052));
			HtmlEntityService.AddSingle(dictionary, "MediumSpace;", HtmlEntityService.Convert(8287));
			HtmlEntityService.AddSingle(dictionary, "Mellintrf;", HtmlEntityService.Convert(8499));
			HtmlEntityService.AddSingle(dictionary, "Mfr;", HtmlEntityService.Convert(120080));
			HtmlEntityService.AddSingle(dictionary, "MinusPlus;", HtmlEntityService.Convert(8723));
			HtmlEntityService.AddSingle(dictionary, "Mopf;", HtmlEntityService.Convert(120132));
			HtmlEntityService.AddSingle(dictionary, "Mscr;", HtmlEntityService.Convert(8499));
			HtmlEntityService.AddSingle(dictionary, "Mu;", HtmlEntityService.Convert(924));
			return dictionary;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00026638 File Offset: 0x00024838
		private Dictionary<string, string> GetSymbolLittleN()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "nabla;", HtmlEntityService.Convert(8711));
			HtmlEntityService.AddSingle(dictionary, "nacute;", HtmlEntityService.Convert(324));
			HtmlEntityService.AddSingle(dictionary, "nang;", HtmlEntityService.Convert(8736, 8402));
			HtmlEntityService.AddSingle(dictionary, "nap;", HtmlEntityService.Convert(8777));
			HtmlEntityService.AddSingle(dictionary, "napE;", HtmlEntityService.Convert(10864, 824));
			HtmlEntityService.AddSingle(dictionary, "napid;", HtmlEntityService.Convert(8779, 824));
			HtmlEntityService.AddSingle(dictionary, "napos;", HtmlEntityService.Convert(329));
			HtmlEntityService.AddSingle(dictionary, "napprox;", HtmlEntityService.Convert(8777));
			HtmlEntityService.AddSingle(dictionary, "natur;", HtmlEntityService.Convert(9838));
			HtmlEntityService.AddSingle(dictionary, "natural;", HtmlEntityService.Convert(9838));
			HtmlEntityService.AddSingle(dictionary, "naturals;", HtmlEntityService.Convert(8469));
			HtmlEntityService.AddBoth(dictionary, "nbsp;", HtmlEntityService.Convert(160));
			HtmlEntityService.AddSingle(dictionary, "nbump;", HtmlEntityService.Convert(8782, 824));
			HtmlEntityService.AddSingle(dictionary, "nbumpe;", HtmlEntityService.Convert(8783, 824));
			HtmlEntityService.AddSingle(dictionary, "ncap;", HtmlEntityService.Convert(10819));
			HtmlEntityService.AddSingle(dictionary, "ncaron;", HtmlEntityService.Convert(328));
			HtmlEntityService.AddSingle(dictionary, "ncedil;", HtmlEntityService.Convert(326));
			HtmlEntityService.AddSingle(dictionary, "ncong;", HtmlEntityService.Convert(8775));
			HtmlEntityService.AddSingle(dictionary, "ncongdot;", HtmlEntityService.Convert(10861, 824));
			HtmlEntityService.AddSingle(dictionary, "ncup;", HtmlEntityService.Convert(10818));
			HtmlEntityService.AddSingle(dictionary, "ncy;", HtmlEntityService.Convert(1085));
			HtmlEntityService.AddSingle(dictionary, "ndash;", HtmlEntityService.Convert(8211));
			HtmlEntityService.AddSingle(dictionary, "ne;", HtmlEntityService.Convert(8800));
			HtmlEntityService.AddSingle(dictionary, "nearhk;", HtmlEntityService.Convert(10532));
			HtmlEntityService.AddSingle(dictionary, "neArr;", HtmlEntityService.Convert(8663));
			HtmlEntityService.AddSingle(dictionary, "nearr;", HtmlEntityService.Convert(8599));
			HtmlEntityService.AddSingle(dictionary, "nearrow;", HtmlEntityService.Convert(8599));
			HtmlEntityService.AddSingle(dictionary, "nedot;", HtmlEntityService.Convert(8784, 824));
			HtmlEntityService.AddSingle(dictionary, "nequiv;", HtmlEntityService.Convert(8802));
			HtmlEntityService.AddSingle(dictionary, "nesear;", HtmlEntityService.Convert(10536));
			HtmlEntityService.AddSingle(dictionary, "nesim;", HtmlEntityService.Convert(8770, 824));
			HtmlEntityService.AddSingle(dictionary, "nexist;", HtmlEntityService.Convert(8708));
			HtmlEntityService.AddSingle(dictionary, "nexists;", HtmlEntityService.Convert(8708));
			HtmlEntityService.AddSingle(dictionary, "nfr;", HtmlEntityService.Convert(120107));
			HtmlEntityService.AddSingle(dictionary, "ngE;", HtmlEntityService.Convert(8807, 824));
			HtmlEntityService.AddSingle(dictionary, "nge;", HtmlEntityService.Convert(8817));
			HtmlEntityService.AddSingle(dictionary, "ngeq;", HtmlEntityService.Convert(8817));
			HtmlEntityService.AddSingle(dictionary, "ngeqq;", HtmlEntityService.Convert(8807, 824));
			HtmlEntityService.AddSingle(dictionary, "ngeqslant;", HtmlEntityService.Convert(10878, 824));
			HtmlEntityService.AddSingle(dictionary, "nges;", HtmlEntityService.Convert(10878, 824));
			HtmlEntityService.AddSingle(dictionary, "nGg;", HtmlEntityService.Convert(8921, 824));
			HtmlEntityService.AddSingle(dictionary, "ngsim;", HtmlEntityService.Convert(8821));
			HtmlEntityService.AddSingle(dictionary, "nGt;", HtmlEntityService.Convert(8811, 8402));
			HtmlEntityService.AddSingle(dictionary, "ngt;", HtmlEntityService.Convert(8815));
			HtmlEntityService.AddSingle(dictionary, "ngtr;", HtmlEntityService.Convert(8815));
			HtmlEntityService.AddSingle(dictionary, "nGtv;", HtmlEntityService.Convert(8811, 824));
			HtmlEntityService.AddSingle(dictionary, "nhArr;", HtmlEntityService.Convert(8654));
			HtmlEntityService.AddSingle(dictionary, "nharr;", HtmlEntityService.Convert(8622));
			HtmlEntityService.AddSingle(dictionary, "nhpar;", HtmlEntityService.Convert(10994));
			HtmlEntityService.AddSingle(dictionary, "ni;", HtmlEntityService.Convert(8715));
			HtmlEntityService.AddSingle(dictionary, "nis;", HtmlEntityService.Convert(8956));
			HtmlEntityService.AddSingle(dictionary, "nisd;", HtmlEntityService.Convert(8954));
			HtmlEntityService.AddSingle(dictionary, "niv;", HtmlEntityService.Convert(8715));
			HtmlEntityService.AddSingle(dictionary, "njcy;", HtmlEntityService.Convert(1114));
			HtmlEntityService.AddSingle(dictionary, "nlArr;", HtmlEntityService.Convert(8653));
			HtmlEntityService.AddSingle(dictionary, "nlarr;", HtmlEntityService.Convert(8602));
			HtmlEntityService.AddSingle(dictionary, "nldr;", HtmlEntityService.Convert(8229));
			HtmlEntityService.AddSingle(dictionary, "nlE;", HtmlEntityService.Convert(8806, 824));
			HtmlEntityService.AddSingle(dictionary, "nle;", HtmlEntityService.Convert(8816));
			HtmlEntityService.AddSingle(dictionary, "nLeftarrow;", HtmlEntityService.Convert(8653));
			HtmlEntityService.AddSingle(dictionary, "nleftarrow;", HtmlEntityService.Convert(8602));
			HtmlEntityService.AddSingle(dictionary, "nLeftrightarrow;", HtmlEntityService.Convert(8654));
			HtmlEntityService.AddSingle(dictionary, "nleftrightarrow;", HtmlEntityService.Convert(8622));
			HtmlEntityService.AddSingle(dictionary, "nleq;", HtmlEntityService.Convert(8816));
			HtmlEntityService.AddSingle(dictionary, "nleqq;", HtmlEntityService.Convert(8806, 824));
			HtmlEntityService.AddSingle(dictionary, "nleqslant;", HtmlEntityService.Convert(10877, 824));
			HtmlEntityService.AddSingle(dictionary, "nles;", HtmlEntityService.Convert(10877, 824));
			HtmlEntityService.AddSingle(dictionary, "nless;", HtmlEntityService.Convert(8814));
			HtmlEntityService.AddSingle(dictionary, "nLl;", HtmlEntityService.Convert(8920, 824));
			HtmlEntityService.AddSingle(dictionary, "nlsim;", HtmlEntityService.Convert(8820));
			HtmlEntityService.AddSingle(dictionary, "nLt;", HtmlEntityService.Convert(8810, 8402));
			HtmlEntityService.AddSingle(dictionary, "nlt;", HtmlEntityService.Convert(8814));
			HtmlEntityService.AddSingle(dictionary, "nltri;", HtmlEntityService.Convert(8938));
			HtmlEntityService.AddSingle(dictionary, "nltrie;", HtmlEntityService.Convert(8940));
			HtmlEntityService.AddSingle(dictionary, "nLtv;", HtmlEntityService.Convert(8810, 824));
			HtmlEntityService.AddSingle(dictionary, "nmid;", HtmlEntityService.Convert(8740));
			HtmlEntityService.AddSingle(dictionary, "nopf;", HtmlEntityService.Convert(120159));
			HtmlEntityService.AddBoth(dictionary, "not;", HtmlEntityService.Convert(172));
			HtmlEntityService.AddSingle(dictionary, "notin;", HtmlEntityService.Convert(8713));
			HtmlEntityService.AddSingle(dictionary, "notindot;", HtmlEntityService.Convert(8949, 824));
			HtmlEntityService.AddSingle(dictionary, "notinE;", HtmlEntityService.Convert(8953, 824));
			HtmlEntityService.AddSingle(dictionary, "notinva;", HtmlEntityService.Convert(8713));
			HtmlEntityService.AddSingle(dictionary, "notinvb;", HtmlEntityService.Convert(8951));
			HtmlEntityService.AddSingle(dictionary, "notinvc;", HtmlEntityService.Convert(8950));
			HtmlEntityService.AddSingle(dictionary, "notni;", HtmlEntityService.Convert(8716));
			HtmlEntityService.AddSingle(dictionary, "notniva;", HtmlEntityService.Convert(8716));
			HtmlEntityService.AddSingle(dictionary, "notnivb;", HtmlEntityService.Convert(8958));
			HtmlEntityService.AddSingle(dictionary, "notnivc;", HtmlEntityService.Convert(8957));
			HtmlEntityService.AddSingle(dictionary, "npar;", HtmlEntityService.Convert(8742));
			HtmlEntityService.AddSingle(dictionary, "nparallel;", HtmlEntityService.Convert(8742));
			HtmlEntityService.AddSingle(dictionary, "nparsl;", HtmlEntityService.Convert(11005, 8421));
			HtmlEntityService.AddSingle(dictionary, "npart;", HtmlEntityService.Convert(8706, 824));
			HtmlEntityService.AddSingle(dictionary, "npolint;", HtmlEntityService.Convert(10772));
			HtmlEntityService.AddSingle(dictionary, "npr;", HtmlEntityService.Convert(8832));
			HtmlEntityService.AddSingle(dictionary, "nprcue;", HtmlEntityService.Convert(8928));
			HtmlEntityService.AddSingle(dictionary, "npre;", HtmlEntityService.Convert(10927, 824));
			HtmlEntityService.AddSingle(dictionary, "nprec;", HtmlEntityService.Convert(8832));
			HtmlEntityService.AddSingle(dictionary, "npreceq;", HtmlEntityService.Convert(10927, 824));
			HtmlEntityService.AddSingle(dictionary, "nrArr;", HtmlEntityService.Convert(8655));
			HtmlEntityService.AddSingle(dictionary, "nrarr;", HtmlEntityService.Convert(8603));
			HtmlEntityService.AddSingle(dictionary, "nrarrc;", HtmlEntityService.Convert(10547, 824));
			HtmlEntityService.AddSingle(dictionary, "nrarrw;", HtmlEntityService.Convert(8605, 824));
			HtmlEntityService.AddSingle(dictionary, "nRightarrow;", HtmlEntityService.Convert(8655));
			HtmlEntityService.AddSingle(dictionary, "nrightarrow;", HtmlEntityService.Convert(8603));
			HtmlEntityService.AddSingle(dictionary, "nrtri;", HtmlEntityService.Convert(8939));
			HtmlEntityService.AddSingle(dictionary, "nrtrie;", HtmlEntityService.Convert(8941));
			HtmlEntityService.AddSingle(dictionary, "nsc;", HtmlEntityService.Convert(8833));
			HtmlEntityService.AddSingle(dictionary, "nsccue;", HtmlEntityService.Convert(8929));
			HtmlEntityService.AddSingle(dictionary, "nsce;", HtmlEntityService.Convert(10928, 824));
			HtmlEntityService.AddSingle(dictionary, "nscr;", HtmlEntityService.Convert(120003));
			HtmlEntityService.AddSingle(dictionary, "nshortmid;", HtmlEntityService.Convert(8740));
			HtmlEntityService.AddSingle(dictionary, "nshortparallel;", HtmlEntityService.Convert(8742));
			HtmlEntityService.AddSingle(dictionary, "nsim;", HtmlEntityService.Convert(8769));
			HtmlEntityService.AddSingle(dictionary, "nsime;", HtmlEntityService.Convert(8772));
			HtmlEntityService.AddSingle(dictionary, "nsimeq;", HtmlEntityService.Convert(8772));
			HtmlEntityService.AddSingle(dictionary, "nsmid;", HtmlEntityService.Convert(8740));
			HtmlEntityService.AddSingle(dictionary, "nspar;", HtmlEntityService.Convert(8742));
			HtmlEntityService.AddSingle(dictionary, "nsqsube;", HtmlEntityService.Convert(8930));
			HtmlEntityService.AddSingle(dictionary, "nsqsupe;", HtmlEntityService.Convert(8931));
			HtmlEntityService.AddSingle(dictionary, "nsub;", HtmlEntityService.Convert(8836));
			HtmlEntityService.AddSingle(dictionary, "nsubE;", HtmlEntityService.Convert(10949, 824));
			HtmlEntityService.AddSingle(dictionary, "nsube;", HtmlEntityService.Convert(8840));
			HtmlEntityService.AddSingle(dictionary, "nsubset;", HtmlEntityService.Convert(8834, 8402));
			HtmlEntityService.AddSingle(dictionary, "nsubseteq;", HtmlEntityService.Convert(8840));
			HtmlEntityService.AddSingle(dictionary, "nsubseteqq;", HtmlEntityService.Convert(10949, 824));
			HtmlEntityService.AddSingle(dictionary, "nsucc;", HtmlEntityService.Convert(8833));
			HtmlEntityService.AddSingle(dictionary, "nsucceq;", HtmlEntityService.Convert(10928, 824));
			HtmlEntityService.AddSingle(dictionary, "nsup;", HtmlEntityService.Convert(8837));
			HtmlEntityService.AddSingle(dictionary, "nsupE;", HtmlEntityService.Convert(10950, 824));
			HtmlEntityService.AddSingle(dictionary, "nsupe;", HtmlEntityService.Convert(8841));
			HtmlEntityService.AddSingle(dictionary, "nsupset;", HtmlEntityService.Convert(8835, 8402));
			HtmlEntityService.AddSingle(dictionary, "nsupseteq;", HtmlEntityService.Convert(8841));
			HtmlEntityService.AddSingle(dictionary, "nsupseteqq;", HtmlEntityService.Convert(10950, 824));
			HtmlEntityService.AddSingle(dictionary, "ntgl;", HtmlEntityService.Convert(8825));
			HtmlEntityService.AddBoth(dictionary, "ntilde;", HtmlEntityService.Convert(241));
			HtmlEntityService.AddSingle(dictionary, "ntlg;", HtmlEntityService.Convert(8824));
			HtmlEntityService.AddSingle(dictionary, "ntriangleleft;", HtmlEntityService.Convert(8938));
			HtmlEntityService.AddSingle(dictionary, "ntrianglelefteq;", HtmlEntityService.Convert(8940));
			HtmlEntityService.AddSingle(dictionary, "ntriangleright;", HtmlEntityService.Convert(8939));
			HtmlEntityService.AddSingle(dictionary, "ntrianglerighteq;", HtmlEntityService.Convert(8941));
			HtmlEntityService.AddSingle(dictionary, "nu;", HtmlEntityService.Convert(957));
			HtmlEntityService.AddSingle(dictionary, "num;", HtmlEntityService.Convert(35));
			HtmlEntityService.AddSingle(dictionary, "numero;", HtmlEntityService.Convert(8470));
			HtmlEntityService.AddSingle(dictionary, "numsp;", HtmlEntityService.Convert(8199));
			HtmlEntityService.AddSingle(dictionary, "nvap;", HtmlEntityService.Convert(8781, 8402));
			HtmlEntityService.AddSingle(dictionary, "nVDash;", HtmlEntityService.Convert(8879));
			HtmlEntityService.AddSingle(dictionary, "nVdash;", HtmlEntityService.Convert(8878));
			HtmlEntityService.AddSingle(dictionary, "nvDash;", HtmlEntityService.Convert(8877));
			HtmlEntityService.AddSingle(dictionary, "nvdash;", HtmlEntityService.Convert(8876));
			HtmlEntityService.AddSingle(dictionary, "nvge;", HtmlEntityService.Convert(8805, 8402));
			HtmlEntityService.AddSingle(dictionary, "nvgt;", HtmlEntityService.Convert(62, 8402));
			HtmlEntityService.AddSingle(dictionary, "nvHarr;", HtmlEntityService.Convert(10500));
			HtmlEntityService.AddSingle(dictionary, "nvinfin;", HtmlEntityService.Convert(10718));
			HtmlEntityService.AddSingle(dictionary, "nvlArr;", HtmlEntityService.Convert(10498));
			HtmlEntityService.AddSingle(dictionary, "nvle;", HtmlEntityService.Convert(8804, 8402));
			HtmlEntityService.AddSingle(dictionary, "nvlt;", HtmlEntityService.Convert(60, 8402));
			HtmlEntityService.AddSingle(dictionary, "nvltrie;", HtmlEntityService.Convert(8884, 8402));
			HtmlEntityService.AddSingle(dictionary, "nvrArr;", HtmlEntityService.Convert(10499));
			HtmlEntityService.AddSingle(dictionary, "nvrtrie;", HtmlEntityService.Convert(8885, 8402));
			HtmlEntityService.AddSingle(dictionary, "nvsim;", HtmlEntityService.Convert(8764, 8402));
			HtmlEntityService.AddSingle(dictionary, "nwarhk;", HtmlEntityService.Convert(10531));
			HtmlEntityService.AddSingle(dictionary, "nwArr;", HtmlEntityService.Convert(8662));
			HtmlEntityService.AddSingle(dictionary, "nwarr;", HtmlEntityService.Convert(8598));
			HtmlEntityService.AddSingle(dictionary, "nwarrow;", HtmlEntityService.Convert(8598));
			HtmlEntityService.AddSingle(dictionary, "nwnear;", HtmlEntityService.Convert(10535));
			return dictionary;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000274B0 File Offset: 0x000256B0
		private Dictionary<string, string> GetSymbolBigN()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Nacute;", HtmlEntityService.Convert(323));
			HtmlEntityService.AddSingle(dictionary, "Ncaron;", HtmlEntityService.Convert(327));
			HtmlEntityService.AddSingle(dictionary, "Ncedil;", HtmlEntityService.Convert(325));
			HtmlEntityService.AddSingle(dictionary, "NegativeMediumSpace;", HtmlEntityService.Convert(8203));
			HtmlEntityService.AddSingle(dictionary, "NegativeThickSpace;", HtmlEntityService.Convert(8203));
			HtmlEntityService.AddSingle(dictionary, "NegativeThinSpace;", HtmlEntityService.Convert(8203));
			HtmlEntityService.AddSingle(dictionary, "NegativeVeryThinSpace;", HtmlEntityService.Convert(8203));
			HtmlEntityService.AddSingle(dictionary, "NestedGreaterGreater;", HtmlEntityService.Convert(8811));
			HtmlEntityService.AddSingle(dictionary, "NestedLessLess;", HtmlEntityService.Convert(8810));
			HtmlEntityService.AddSingle(dictionary, "Ncy;", HtmlEntityService.Convert(1053));
			HtmlEntityService.AddSingle(dictionary, "Nfr;", HtmlEntityService.Convert(120081));
			HtmlEntityService.AddSingle(dictionary, "NoBreak;", HtmlEntityService.Convert(8288));
			HtmlEntityService.AddSingle(dictionary, "NonBreakingSpace;", HtmlEntityService.Convert(160));
			HtmlEntityService.AddSingle(dictionary, "Nopf;", HtmlEntityService.Convert(8469));
			HtmlEntityService.AddSingle(dictionary, "NewLine;", HtmlEntityService.Convert(10));
			HtmlEntityService.AddSingle(dictionary, "NJcy;", HtmlEntityService.Convert(1034));
			HtmlEntityService.AddSingle(dictionary, "Not;", HtmlEntityService.Convert(10988));
			HtmlEntityService.AddSingle(dictionary, "NotCongruent;", HtmlEntityService.Convert(8802));
			HtmlEntityService.AddSingle(dictionary, "NotCupCap;", HtmlEntityService.Convert(8813));
			HtmlEntityService.AddSingle(dictionary, "NotDoubleVerticalBar;", HtmlEntityService.Convert(8742));
			HtmlEntityService.AddSingle(dictionary, "NotElement;", HtmlEntityService.Convert(8713));
			HtmlEntityService.AddSingle(dictionary, "NotEqual;", HtmlEntityService.Convert(8800));
			HtmlEntityService.AddSingle(dictionary, "NotEqualTilde;", HtmlEntityService.Convert(8770, 824));
			HtmlEntityService.AddSingle(dictionary, "NotExists;", HtmlEntityService.Convert(8708));
			HtmlEntityService.AddSingle(dictionary, "NotGreater;", HtmlEntityService.Convert(8815));
			HtmlEntityService.AddSingle(dictionary, "NotGreaterEqual;", HtmlEntityService.Convert(8817));
			HtmlEntityService.AddSingle(dictionary, "NotGreaterFullEqual;", HtmlEntityService.Convert(8807, 824));
			HtmlEntityService.AddSingle(dictionary, "NotGreaterGreater;", HtmlEntityService.Convert(8811, 824));
			HtmlEntityService.AddSingle(dictionary, "NotGreaterLess;", HtmlEntityService.Convert(8825));
			HtmlEntityService.AddSingle(dictionary, "NotGreaterSlantEqual;", HtmlEntityService.Convert(10878, 824));
			HtmlEntityService.AddSingle(dictionary, "NotGreaterTilde;", HtmlEntityService.Convert(8821));
			HtmlEntityService.AddSingle(dictionary, "NotHumpDownHump;", HtmlEntityService.Convert(8782, 824));
			HtmlEntityService.AddSingle(dictionary, "NotHumpEqual;", HtmlEntityService.Convert(8783, 824));
			HtmlEntityService.AddSingle(dictionary, "NotLeftTriangle;", HtmlEntityService.Convert(8938));
			HtmlEntityService.AddSingle(dictionary, "NotLeftTriangleBar;", HtmlEntityService.Convert(10703, 824));
			HtmlEntityService.AddSingle(dictionary, "NotLeftTriangleEqual;", HtmlEntityService.Convert(8940));
			HtmlEntityService.AddSingle(dictionary, "NotLess;", HtmlEntityService.Convert(8814));
			HtmlEntityService.AddSingle(dictionary, "NotLessEqual;", HtmlEntityService.Convert(8816));
			HtmlEntityService.AddSingle(dictionary, "NotLessGreater;", HtmlEntityService.Convert(8824));
			HtmlEntityService.AddSingle(dictionary, "NotLessLess;", HtmlEntityService.Convert(8810, 824));
			HtmlEntityService.AddSingle(dictionary, "NotLessSlantEqual;", HtmlEntityService.Convert(10877, 824));
			HtmlEntityService.AddSingle(dictionary, "NotLessTilde;", HtmlEntityService.Convert(8820));
			HtmlEntityService.AddSingle(dictionary, "NotNestedGreaterGreater;", HtmlEntityService.Convert(10914, 824));
			HtmlEntityService.AddSingle(dictionary, "NotNestedLessLess;", HtmlEntityService.Convert(10913, 824));
			HtmlEntityService.AddSingle(dictionary, "NotPrecedes;", HtmlEntityService.Convert(8832));
			HtmlEntityService.AddSingle(dictionary, "NotPrecedesEqual;", HtmlEntityService.Convert(10927, 824));
			HtmlEntityService.AddSingle(dictionary, "NotPrecedesSlantEqual;", HtmlEntityService.Convert(8928));
			HtmlEntityService.AddSingle(dictionary, "NotReverseElement;", HtmlEntityService.Convert(8716));
			HtmlEntityService.AddSingle(dictionary, "NotRightTriangle;", HtmlEntityService.Convert(8939));
			HtmlEntityService.AddSingle(dictionary, "NotRightTriangleBar;", HtmlEntityService.Convert(10704, 824));
			HtmlEntityService.AddSingle(dictionary, "NotRightTriangleEqual;", HtmlEntityService.Convert(8941));
			HtmlEntityService.AddSingle(dictionary, "NotSquareSubset;", HtmlEntityService.Convert(8847, 824));
			HtmlEntityService.AddSingle(dictionary, "NotSquareSubsetEqual;", HtmlEntityService.Convert(8930));
			HtmlEntityService.AddSingle(dictionary, "NotSquareSuperset;", HtmlEntityService.Convert(8848, 824));
			HtmlEntityService.AddSingle(dictionary, "NotSquareSupersetEqual;", HtmlEntityService.Convert(8931));
			HtmlEntityService.AddSingle(dictionary, "NotSubset;", HtmlEntityService.Convert(8834, 8402));
			HtmlEntityService.AddSingle(dictionary, "NotSubsetEqual;", HtmlEntityService.Convert(8840));
			HtmlEntityService.AddSingle(dictionary, "NotSucceeds;", HtmlEntityService.Convert(8833));
			HtmlEntityService.AddSingle(dictionary, "NotSucceedsEqual;", HtmlEntityService.Convert(10928, 824));
			HtmlEntityService.AddSingle(dictionary, "NotSucceedsSlantEqual;", HtmlEntityService.Convert(8929));
			HtmlEntityService.AddSingle(dictionary, "NotSucceedsTilde;", HtmlEntityService.Convert(8831, 824));
			HtmlEntityService.AddSingle(dictionary, "NotSuperset;", HtmlEntityService.Convert(8835, 8402));
			HtmlEntityService.AddSingle(dictionary, "NotSupersetEqual;", HtmlEntityService.Convert(8841));
			HtmlEntityService.AddSingle(dictionary, "NotTilde;", HtmlEntityService.Convert(8769));
			HtmlEntityService.AddSingle(dictionary, "NotTildeEqual;", HtmlEntityService.Convert(8772));
			HtmlEntityService.AddSingle(dictionary, "NotTildeFullEqual;", HtmlEntityService.Convert(8775));
			HtmlEntityService.AddSingle(dictionary, "NotTildeTilde;", HtmlEntityService.Convert(8777));
			HtmlEntityService.AddSingle(dictionary, "NotVerticalBar;", HtmlEntityService.Convert(8740));
			HtmlEntityService.AddSingle(dictionary, "Nscr;", HtmlEntityService.Convert(119977));
			HtmlEntityService.AddBoth(dictionary, "Ntilde;", HtmlEntityService.Convert(209));
			HtmlEntityService.AddSingle(dictionary, "Nu;", HtmlEntityService.Convert(925));
			return dictionary;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00027AF4 File Offset: 0x00025CF4
		private Dictionary<string, string> GetSymbolLittleO()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "oacute;", HtmlEntityService.Convert(243));
			HtmlEntityService.AddSingle(dictionary, "oast;", HtmlEntityService.Convert(8859));
			HtmlEntityService.AddSingle(dictionary, "ocir;", HtmlEntityService.Convert(8858));
			HtmlEntityService.AddBoth(dictionary, "ocirc;", HtmlEntityService.Convert(244));
			HtmlEntityService.AddSingle(dictionary, "ocy;", HtmlEntityService.Convert(1086));
			HtmlEntityService.AddSingle(dictionary, "odash;", HtmlEntityService.Convert(8861));
			HtmlEntityService.AddSingle(dictionary, "odblac;", HtmlEntityService.Convert(337));
			HtmlEntityService.AddSingle(dictionary, "odiv;", HtmlEntityService.Convert(10808));
			HtmlEntityService.AddSingle(dictionary, "odot;", HtmlEntityService.Convert(8857));
			HtmlEntityService.AddSingle(dictionary, "odsold;", HtmlEntityService.Convert(10684));
			HtmlEntityService.AddSingle(dictionary, "oelig;", HtmlEntityService.Convert(339));
			HtmlEntityService.AddSingle(dictionary, "ofcir;", HtmlEntityService.Convert(10687));
			HtmlEntityService.AddSingle(dictionary, "ofr;", HtmlEntityService.Convert(120108));
			HtmlEntityService.AddSingle(dictionary, "ogon;", HtmlEntityService.Convert(731));
			HtmlEntityService.AddBoth(dictionary, "ograve;", HtmlEntityService.Convert(242));
			HtmlEntityService.AddSingle(dictionary, "ogt;", HtmlEntityService.Convert(10689));
			HtmlEntityService.AddSingle(dictionary, "ohbar;", HtmlEntityService.Convert(10677));
			HtmlEntityService.AddSingle(dictionary, "ohm;", HtmlEntityService.Convert(937));
			HtmlEntityService.AddSingle(dictionary, "oint;", HtmlEntityService.Convert(8750));
			HtmlEntityService.AddSingle(dictionary, "olarr;", HtmlEntityService.Convert(8634));
			HtmlEntityService.AddSingle(dictionary, "olcir;", HtmlEntityService.Convert(10686));
			HtmlEntityService.AddSingle(dictionary, "olcross;", HtmlEntityService.Convert(10683));
			HtmlEntityService.AddSingle(dictionary, "oline;", HtmlEntityService.Convert(8254));
			HtmlEntityService.AddSingle(dictionary, "olt;", HtmlEntityService.Convert(10688));
			HtmlEntityService.AddSingle(dictionary, "omacr;", HtmlEntityService.Convert(333));
			HtmlEntityService.AddSingle(dictionary, "omega;", HtmlEntityService.Convert(969));
			HtmlEntityService.AddSingle(dictionary, "omicron;", HtmlEntityService.Convert(959));
			HtmlEntityService.AddSingle(dictionary, "omid;", HtmlEntityService.Convert(10678));
			HtmlEntityService.AddSingle(dictionary, "ominus;", HtmlEntityService.Convert(8854));
			HtmlEntityService.AddSingle(dictionary, "oopf;", HtmlEntityService.Convert(120160));
			HtmlEntityService.AddSingle(dictionary, "opar;", HtmlEntityService.Convert(10679));
			HtmlEntityService.AddSingle(dictionary, "operp;", HtmlEntityService.Convert(10681));
			HtmlEntityService.AddSingle(dictionary, "oplus;", HtmlEntityService.Convert(8853));
			HtmlEntityService.AddSingle(dictionary, "or;", HtmlEntityService.Convert(8744));
			HtmlEntityService.AddSingle(dictionary, "orarr;", HtmlEntityService.Convert(8635));
			HtmlEntityService.AddSingle(dictionary, "ord;", HtmlEntityService.Convert(10845));
			HtmlEntityService.AddSingle(dictionary, "order;", HtmlEntityService.Convert(8500));
			HtmlEntityService.AddSingle(dictionary, "orderof;", HtmlEntityService.Convert(8500));
			HtmlEntityService.AddBoth(dictionary, "ordf;", HtmlEntityService.Convert(170));
			HtmlEntityService.AddBoth(dictionary, "ordm;", HtmlEntityService.Convert(186));
			HtmlEntityService.AddSingle(dictionary, "origof;", HtmlEntityService.Convert(8886));
			HtmlEntityService.AddSingle(dictionary, "oror;", HtmlEntityService.Convert(10838));
			HtmlEntityService.AddSingle(dictionary, "orslope;", HtmlEntityService.Convert(10839));
			HtmlEntityService.AddSingle(dictionary, "orv;", HtmlEntityService.Convert(10843));
			HtmlEntityService.AddSingle(dictionary, "oS;", HtmlEntityService.Convert(9416));
			HtmlEntityService.AddSingle(dictionary, "oscr;", HtmlEntityService.Convert(8500));
			HtmlEntityService.AddBoth(dictionary, "oslash;", HtmlEntityService.Convert(248));
			HtmlEntityService.AddSingle(dictionary, "osol;", HtmlEntityService.Convert(8856));
			HtmlEntityService.AddBoth(dictionary, "otilde;", HtmlEntityService.Convert(245));
			HtmlEntityService.AddSingle(dictionary, "otimes;", HtmlEntityService.Convert(8855));
			HtmlEntityService.AddSingle(dictionary, "otimesas;", HtmlEntityService.Convert(10806));
			HtmlEntityService.AddBoth(dictionary, "ouml;", HtmlEntityService.Convert(246));
			HtmlEntityService.AddSingle(dictionary, "ovbar;", HtmlEntityService.Convert(9021));
			return dictionary;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00027F60 File Offset: 0x00026160
		private Dictionary<string, string> GetSymbolBigO()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "Oacute;", HtmlEntityService.Convert(211));
			HtmlEntityService.AddBoth(dictionary, "Ocirc;", HtmlEntityService.Convert(212));
			HtmlEntityService.AddSingle(dictionary, "Ocy;", HtmlEntityService.Convert(1054));
			HtmlEntityService.AddSingle(dictionary, "Odblac;", HtmlEntityService.Convert(336));
			HtmlEntityService.AddSingle(dictionary, "OElig;", HtmlEntityService.Convert(338));
			HtmlEntityService.AddSingle(dictionary, "Ofr;", HtmlEntityService.Convert(120082));
			HtmlEntityService.AddBoth(dictionary, "Ograve;", HtmlEntityService.Convert(210));
			HtmlEntityService.AddSingle(dictionary, "Omacr;", HtmlEntityService.Convert(332));
			HtmlEntityService.AddSingle(dictionary, "Omega;", HtmlEntityService.Convert(937));
			HtmlEntityService.AddSingle(dictionary, "Omicron;", HtmlEntityService.Convert(927));
			HtmlEntityService.AddSingle(dictionary, "Oopf;", HtmlEntityService.Convert(120134));
			HtmlEntityService.AddSingle(dictionary, "OpenCurlyDoubleQuote;", HtmlEntityService.Convert(8220));
			HtmlEntityService.AddSingle(dictionary, "OpenCurlyQuote;", HtmlEntityService.Convert(8216));
			HtmlEntityService.AddSingle(dictionary, "Or;", HtmlEntityService.Convert(10836));
			HtmlEntityService.AddSingle(dictionary, "Oscr;", HtmlEntityService.Convert(119978));
			HtmlEntityService.AddBoth(dictionary, "Oslash;", HtmlEntityService.Convert(216));
			HtmlEntityService.AddBoth(dictionary, "Otilde;", HtmlEntityService.Convert(213));
			HtmlEntityService.AddSingle(dictionary, "Otimes;", HtmlEntityService.Convert(10807));
			HtmlEntityService.AddBoth(dictionary, "Ouml;", HtmlEntityService.Convert(214));
			HtmlEntityService.AddSingle(dictionary, "OverBar;", HtmlEntityService.Convert(8254));
			HtmlEntityService.AddSingle(dictionary, "OverBrace;", HtmlEntityService.Convert(9182));
			HtmlEntityService.AddSingle(dictionary, "OverBracket;", HtmlEntityService.Convert(9140));
			HtmlEntityService.AddSingle(dictionary, "OverParenthesis;", HtmlEntityService.Convert(9180));
			return dictionary;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00028158 File Offset: 0x00026358
		private Dictionary<string, string> GetSymbolLittleP()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "pfr;", HtmlEntityService.Convert(120109));
			HtmlEntityService.AddSingle(dictionary, "par;", HtmlEntityService.Convert(8741));
			HtmlEntityService.AddBoth(dictionary, "para;", HtmlEntityService.Convert(182));
			HtmlEntityService.AddSingle(dictionary, "parallel;", HtmlEntityService.Convert(8741));
			HtmlEntityService.AddSingle(dictionary, "parsim;", HtmlEntityService.Convert(10995));
			HtmlEntityService.AddSingle(dictionary, "parsl;", HtmlEntityService.Convert(11005));
			HtmlEntityService.AddSingle(dictionary, "part;", HtmlEntityService.Convert(8706));
			HtmlEntityService.AddSingle(dictionary, "pcy;", HtmlEntityService.Convert(1087));
			HtmlEntityService.AddSingle(dictionary, "percnt;", HtmlEntityService.Convert(37));
			HtmlEntityService.AddSingle(dictionary, "period;", HtmlEntityService.Convert(46));
			HtmlEntityService.AddSingle(dictionary, "permil;", HtmlEntityService.Convert(8240));
			HtmlEntityService.AddSingle(dictionary, "perp;", HtmlEntityService.Convert(8869));
			HtmlEntityService.AddSingle(dictionary, "pertenk;", HtmlEntityService.Convert(8241));
			HtmlEntityService.AddSingle(dictionary, "phi;", HtmlEntityService.Convert(966));
			HtmlEntityService.AddSingle(dictionary, "phiv;", HtmlEntityService.Convert(981));
			HtmlEntityService.AddSingle(dictionary, "phmmat;", HtmlEntityService.Convert(8499));
			HtmlEntityService.AddSingle(dictionary, "phone;", HtmlEntityService.Convert(9742));
			HtmlEntityService.AddSingle(dictionary, "pi;", HtmlEntityService.Convert(960));
			HtmlEntityService.AddSingle(dictionary, "pitchfork;", HtmlEntityService.Convert(8916));
			HtmlEntityService.AddSingle(dictionary, "piv;", HtmlEntityService.Convert(982));
			HtmlEntityService.AddSingle(dictionary, "planck;", HtmlEntityService.Convert(8463));
			HtmlEntityService.AddSingle(dictionary, "planckh;", HtmlEntityService.Convert(8462));
			HtmlEntityService.AddSingle(dictionary, "plankv;", HtmlEntityService.Convert(8463));
			HtmlEntityService.AddSingle(dictionary, "plus;", HtmlEntityService.Convert(43));
			HtmlEntityService.AddSingle(dictionary, "plusacir;", HtmlEntityService.Convert(10787));
			HtmlEntityService.AddSingle(dictionary, "plusb;", HtmlEntityService.Convert(8862));
			HtmlEntityService.AddSingle(dictionary, "pluscir;", HtmlEntityService.Convert(10786));
			HtmlEntityService.AddSingle(dictionary, "plusdo;", HtmlEntityService.Convert(8724));
			HtmlEntityService.AddSingle(dictionary, "plusdu;", HtmlEntityService.Convert(10789));
			HtmlEntityService.AddSingle(dictionary, "pluse;", HtmlEntityService.Convert(10866));
			HtmlEntityService.AddBoth(dictionary, "plusmn;", HtmlEntityService.Convert(177));
			HtmlEntityService.AddSingle(dictionary, "plussim;", HtmlEntityService.Convert(10790));
			HtmlEntityService.AddSingle(dictionary, "plustwo;", HtmlEntityService.Convert(10791));
			HtmlEntityService.AddSingle(dictionary, "pm;", HtmlEntityService.Convert(177));
			HtmlEntityService.AddSingle(dictionary, "pointint;", HtmlEntityService.Convert(10773));
			HtmlEntityService.AddSingle(dictionary, "popf;", HtmlEntityService.Convert(120161));
			HtmlEntityService.AddBoth(dictionary, "pound;", HtmlEntityService.Convert(163));
			HtmlEntityService.AddSingle(dictionary, "pr;", HtmlEntityService.Convert(8826));
			HtmlEntityService.AddSingle(dictionary, "prap;", HtmlEntityService.Convert(10935));
			HtmlEntityService.AddSingle(dictionary, "prcue;", HtmlEntityService.Convert(8828));
			HtmlEntityService.AddSingle(dictionary, "prE;", HtmlEntityService.Convert(10931));
			HtmlEntityService.AddSingle(dictionary, "pre;", HtmlEntityService.Convert(10927));
			HtmlEntityService.AddSingle(dictionary, "prec;", HtmlEntityService.Convert(8826));
			HtmlEntityService.AddSingle(dictionary, "precapprox;", HtmlEntityService.Convert(10935));
			HtmlEntityService.AddSingle(dictionary, "preccurlyeq;", HtmlEntityService.Convert(8828));
			HtmlEntityService.AddSingle(dictionary, "preceq;", HtmlEntityService.Convert(10927));
			HtmlEntityService.AddSingle(dictionary, "precnapprox;", HtmlEntityService.Convert(10937));
			HtmlEntityService.AddSingle(dictionary, "precneqq;", HtmlEntityService.Convert(10933));
			HtmlEntityService.AddSingle(dictionary, "precnsim;", HtmlEntityService.Convert(8936));
			HtmlEntityService.AddSingle(dictionary, "precsim;", HtmlEntityService.Convert(8830));
			HtmlEntityService.AddSingle(dictionary, "prime;", HtmlEntityService.Convert(8242));
			HtmlEntityService.AddSingle(dictionary, "primes;", HtmlEntityService.Convert(8473));
			HtmlEntityService.AddSingle(dictionary, "prnap;", HtmlEntityService.Convert(10937));
			HtmlEntityService.AddSingle(dictionary, "prnE;", HtmlEntityService.Convert(10933));
			HtmlEntityService.AddSingle(dictionary, "prnsim;", HtmlEntityService.Convert(8936));
			HtmlEntityService.AddSingle(dictionary, "prod;", HtmlEntityService.Convert(8719));
			HtmlEntityService.AddSingle(dictionary, "profalar;", HtmlEntityService.Convert(9006));
			HtmlEntityService.AddSingle(dictionary, "profline;", HtmlEntityService.Convert(8978));
			HtmlEntityService.AddSingle(dictionary, "profsurf;", HtmlEntityService.Convert(8979));
			HtmlEntityService.AddSingle(dictionary, "prop;", HtmlEntityService.Convert(8733));
			HtmlEntityService.AddSingle(dictionary, "propto;", HtmlEntityService.Convert(8733));
			HtmlEntityService.AddSingle(dictionary, "prsim;", HtmlEntityService.Convert(8830));
			HtmlEntityService.AddSingle(dictionary, "prurel;", HtmlEntityService.Convert(8880));
			HtmlEntityService.AddSingle(dictionary, "pscr;", HtmlEntityService.Convert(120005));
			HtmlEntityService.AddSingle(dictionary, "psi;", HtmlEntityService.Convert(968));
			HtmlEntityService.AddSingle(dictionary, "puncsp;", HtmlEntityService.Convert(8200));
			return dictionary;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000286CC File Offset: 0x000268CC
		private Dictionary<string, string> GetSymbolBigP()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "PartialD;", HtmlEntityService.Convert(8706));
			HtmlEntityService.AddSingle(dictionary, "Pcy;", HtmlEntityService.Convert(1055));
			HtmlEntityService.AddSingle(dictionary, "Pfr;", HtmlEntityService.Convert(120083));
			HtmlEntityService.AddSingle(dictionary, "Phi;", HtmlEntityService.Convert(934));
			HtmlEntityService.AddSingle(dictionary, "Pi;", HtmlEntityService.Convert(928));
			HtmlEntityService.AddSingle(dictionary, "PlusMinus;", HtmlEntityService.Convert(177));
			HtmlEntityService.AddSingle(dictionary, "Poincareplane;", HtmlEntityService.Convert(8460));
			HtmlEntityService.AddSingle(dictionary, "Popf;", HtmlEntityService.Convert(8473));
			HtmlEntityService.AddSingle(dictionary, "Pr;", HtmlEntityService.Convert(10939));
			HtmlEntityService.AddSingle(dictionary, "Precedes;", HtmlEntityService.Convert(8826));
			HtmlEntityService.AddSingle(dictionary, "PrecedesEqual;", HtmlEntityService.Convert(10927));
			HtmlEntityService.AddSingle(dictionary, "PrecedesSlantEqual;", HtmlEntityService.Convert(8828));
			HtmlEntityService.AddSingle(dictionary, "PrecedesTilde;", HtmlEntityService.Convert(8830));
			HtmlEntityService.AddSingle(dictionary, "Prime;", HtmlEntityService.Convert(8243));
			HtmlEntityService.AddSingle(dictionary, "Product;", HtmlEntityService.Convert(8719));
			HtmlEntityService.AddSingle(dictionary, "Proportion;", HtmlEntityService.Convert(8759));
			HtmlEntityService.AddSingle(dictionary, "Proportional;", HtmlEntityService.Convert(8733));
			HtmlEntityService.AddSingle(dictionary, "Pscr;", HtmlEntityService.Convert(119979));
			HtmlEntityService.AddSingle(dictionary, "Psi;", HtmlEntityService.Convert(936));
			return dictionary;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00028870 File Offset: 0x00026A70
		private Dictionary<string, string> GetSymbolLittleQ()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "qfr;", HtmlEntityService.Convert(120110));
			HtmlEntityService.AddSingle(dictionary, "qint;", HtmlEntityService.Convert(10764));
			HtmlEntityService.AddSingle(dictionary, "qopf;", HtmlEntityService.Convert(120162));
			HtmlEntityService.AddSingle(dictionary, "qprime;", HtmlEntityService.Convert(8279));
			HtmlEntityService.AddSingle(dictionary, "qscr;", HtmlEntityService.Convert(120006));
			HtmlEntityService.AddSingle(dictionary, "quaternions;", HtmlEntityService.Convert(8461));
			HtmlEntityService.AddSingle(dictionary, "quatint;", HtmlEntityService.Convert(10774));
			HtmlEntityService.AddSingle(dictionary, "quest;", HtmlEntityService.Convert(63));
			HtmlEntityService.AddSingle(dictionary, "questeq;", HtmlEntityService.Convert(8799));
			HtmlEntityService.AddBoth(dictionary, "quot;", HtmlEntityService.Convert(34));
			return dictionary;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00028950 File Offset: 0x00026B50
		private Dictionary<string, string> GetSymbolBigQ()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Qfr;", HtmlEntityService.Convert(120084));
			HtmlEntityService.AddSingle(dictionary, "Qopf;", HtmlEntityService.Convert(8474));
			HtmlEntityService.AddSingle(dictionary, "Qscr;", HtmlEntityService.Convert(119980));
			HtmlEntityService.AddBoth(dictionary, "QUOT;", HtmlEntityService.Convert(34));
			return dictionary;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000289B4 File Offset: 0x00026BB4
		private Dictionary<string, string> GetSymbolLittleR()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "rAarr;", HtmlEntityService.Convert(8667));
			HtmlEntityService.AddSingle(dictionary, "race;", HtmlEntityService.Convert(8765, 817));
			HtmlEntityService.AddSingle(dictionary, "racute;", HtmlEntityService.Convert(341));
			HtmlEntityService.AddSingle(dictionary, "radic;", HtmlEntityService.Convert(8730));
			HtmlEntityService.AddSingle(dictionary, "raemptyv;", HtmlEntityService.Convert(10675));
			HtmlEntityService.AddSingle(dictionary, "rang;", HtmlEntityService.Convert(10217));
			HtmlEntityService.AddSingle(dictionary, "rangd;", HtmlEntityService.Convert(10642));
			HtmlEntityService.AddSingle(dictionary, "range;", HtmlEntityService.Convert(10661));
			HtmlEntityService.AddSingle(dictionary, "rangle;", HtmlEntityService.Convert(10217));
			HtmlEntityService.AddBoth(dictionary, "raquo;", HtmlEntityService.Convert(187));
			HtmlEntityService.AddSingle(dictionary, "rArr;", HtmlEntityService.Convert(8658));
			HtmlEntityService.AddSingle(dictionary, "rarr;", HtmlEntityService.Convert(8594));
			HtmlEntityService.AddSingle(dictionary, "rarrap;", HtmlEntityService.Convert(10613));
			HtmlEntityService.AddSingle(dictionary, "rarrb;", HtmlEntityService.Convert(8677));
			HtmlEntityService.AddSingle(dictionary, "rarrbfs;", HtmlEntityService.Convert(10528));
			HtmlEntityService.AddSingle(dictionary, "rarrc;", HtmlEntityService.Convert(10547));
			HtmlEntityService.AddSingle(dictionary, "rarrfs;", HtmlEntityService.Convert(10526));
			HtmlEntityService.AddSingle(dictionary, "rarrhk;", HtmlEntityService.Convert(8618));
			HtmlEntityService.AddSingle(dictionary, "rarrlp;", HtmlEntityService.Convert(8620));
			HtmlEntityService.AddSingle(dictionary, "rarrpl;", HtmlEntityService.Convert(10565));
			HtmlEntityService.AddSingle(dictionary, "rarrsim;", HtmlEntityService.Convert(10612));
			HtmlEntityService.AddSingle(dictionary, "rarrtl;", HtmlEntityService.Convert(8611));
			HtmlEntityService.AddSingle(dictionary, "rarrw;", HtmlEntityService.Convert(8605));
			HtmlEntityService.AddSingle(dictionary, "rAtail;", HtmlEntityService.Convert(10524));
			HtmlEntityService.AddSingle(dictionary, "ratail;", HtmlEntityService.Convert(10522));
			HtmlEntityService.AddSingle(dictionary, "ratio;", HtmlEntityService.Convert(8758));
			HtmlEntityService.AddSingle(dictionary, "rationals;", HtmlEntityService.Convert(8474));
			HtmlEntityService.AddSingle(dictionary, "rBarr;", HtmlEntityService.Convert(10511));
			HtmlEntityService.AddSingle(dictionary, "rbarr;", HtmlEntityService.Convert(10509));
			HtmlEntityService.AddSingle(dictionary, "rbbrk;", HtmlEntityService.Convert(10099));
			HtmlEntityService.AddSingle(dictionary, "rbrace;", HtmlEntityService.Convert(125));
			HtmlEntityService.AddSingle(dictionary, "rbrack;", HtmlEntityService.Convert(93));
			HtmlEntityService.AddSingle(dictionary, "rbrke;", HtmlEntityService.Convert(10636));
			HtmlEntityService.AddSingle(dictionary, "rbrksld;", HtmlEntityService.Convert(10638));
			HtmlEntityService.AddSingle(dictionary, "rbrkslu;", HtmlEntityService.Convert(10640));
			HtmlEntityService.AddSingle(dictionary, "rcaron;", HtmlEntityService.Convert(345));
			HtmlEntityService.AddSingle(dictionary, "rcedil;", HtmlEntityService.Convert(343));
			HtmlEntityService.AddSingle(dictionary, "rceil;", HtmlEntityService.Convert(8969));
			HtmlEntityService.AddSingle(dictionary, "rcub;", HtmlEntityService.Convert(125));
			HtmlEntityService.AddSingle(dictionary, "rcy;", HtmlEntityService.Convert(1088));
			HtmlEntityService.AddSingle(dictionary, "rdca;", HtmlEntityService.Convert(10551));
			HtmlEntityService.AddSingle(dictionary, "rdldhar;", HtmlEntityService.Convert(10601));
			HtmlEntityService.AddSingle(dictionary, "rdquo;", HtmlEntityService.Convert(8221));
			HtmlEntityService.AddSingle(dictionary, "rdquor;", HtmlEntityService.Convert(8221));
			HtmlEntityService.AddSingle(dictionary, "rdsh;", HtmlEntityService.Convert(8627));
			HtmlEntityService.AddSingle(dictionary, "real;", HtmlEntityService.Convert(8476));
			HtmlEntityService.AddSingle(dictionary, "realine;", HtmlEntityService.Convert(8475));
			HtmlEntityService.AddSingle(dictionary, "realpart;", HtmlEntityService.Convert(8476));
			HtmlEntityService.AddSingle(dictionary, "reals;", HtmlEntityService.Convert(8477));
			HtmlEntityService.AddSingle(dictionary, "rect;", HtmlEntityService.Convert(9645));
			HtmlEntityService.AddBoth(dictionary, "reg;", HtmlEntityService.Convert(174));
			HtmlEntityService.AddSingle(dictionary, "rfisht;", HtmlEntityService.Convert(10621));
			HtmlEntityService.AddSingle(dictionary, "rfloor;", HtmlEntityService.Convert(8971));
			HtmlEntityService.AddSingle(dictionary, "rfr;", HtmlEntityService.Convert(120111));
			HtmlEntityService.AddSingle(dictionary, "rHar;", HtmlEntityService.Convert(10596));
			HtmlEntityService.AddSingle(dictionary, "rhard;", HtmlEntityService.Convert(8641));
			HtmlEntityService.AddSingle(dictionary, "rharu;", HtmlEntityService.Convert(8640));
			HtmlEntityService.AddSingle(dictionary, "rharul;", HtmlEntityService.Convert(10604));
			HtmlEntityService.AddSingle(dictionary, "rho;", HtmlEntityService.Convert(961));
			HtmlEntityService.AddSingle(dictionary, "rhov;", HtmlEntityService.Convert(1009));
			HtmlEntityService.AddSingle(dictionary, "rightarrow;", HtmlEntityService.Convert(8594));
			HtmlEntityService.AddSingle(dictionary, "rightarrowtail;", HtmlEntityService.Convert(8611));
			HtmlEntityService.AddSingle(dictionary, "rightharpoondown;", HtmlEntityService.Convert(8641));
			HtmlEntityService.AddSingle(dictionary, "rightharpoonup;", HtmlEntityService.Convert(8640));
			HtmlEntityService.AddSingle(dictionary, "rightleftarrows;", HtmlEntityService.Convert(8644));
			HtmlEntityService.AddSingle(dictionary, "rightleftharpoons;", HtmlEntityService.Convert(8652));
			HtmlEntityService.AddSingle(dictionary, "rightrightarrows;", HtmlEntityService.Convert(8649));
			HtmlEntityService.AddSingle(dictionary, "rightsquigarrow;", HtmlEntityService.Convert(8605));
			HtmlEntityService.AddSingle(dictionary, "rightthreetimes;", HtmlEntityService.Convert(8908));
			HtmlEntityService.AddSingle(dictionary, "ring;", HtmlEntityService.Convert(730));
			HtmlEntityService.AddSingle(dictionary, "risingdotseq;", HtmlEntityService.Convert(8787));
			HtmlEntityService.AddSingle(dictionary, "rlarr;", HtmlEntityService.Convert(8644));
			HtmlEntityService.AddSingle(dictionary, "rlhar;", HtmlEntityService.Convert(8652));
			HtmlEntityService.AddSingle(dictionary, "rlm;", HtmlEntityService.Convert(8207));
			HtmlEntityService.AddSingle(dictionary, "rmoust;", HtmlEntityService.Convert(9137));
			HtmlEntityService.AddSingle(dictionary, "rmoustache;", HtmlEntityService.Convert(9137));
			HtmlEntityService.AddSingle(dictionary, "rnmid;", HtmlEntityService.Convert(10990));
			HtmlEntityService.AddSingle(dictionary, "roang;", HtmlEntityService.Convert(10221));
			HtmlEntityService.AddSingle(dictionary, "roarr;", HtmlEntityService.Convert(8702));
			HtmlEntityService.AddSingle(dictionary, "robrk;", HtmlEntityService.Convert(10215));
			HtmlEntityService.AddSingle(dictionary, "ropar;", HtmlEntityService.Convert(10630));
			HtmlEntityService.AddSingle(dictionary, "ropf;", HtmlEntityService.Convert(120163));
			HtmlEntityService.AddSingle(dictionary, "roplus;", HtmlEntityService.Convert(10798));
			HtmlEntityService.AddSingle(dictionary, "rotimes;", HtmlEntityService.Convert(10805));
			HtmlEntityService.AddSingle(dictionary, "rpar;", HtmlEntityService.Convert(41));
			HtmlEntityService.AddSingle(dictionary, "rpargt;", HtmlEntityService.Convert(10644));
			HtmlEntityService.AddSingle(dictionary, "rppolint;", HtmlEntityService.Convert(10770));
			HtmlEntityService.AddSingle(dictionary, "rrarr;", HtmlEntityService.Convert(8649));
			HtmlEntityService.AddSingle(dictionary, "rsaquo;", HtmlEntityService.Convert(8250));
			HtmlEntityService.AddSingle(dictionary, "rscr;", HtmlEntityService.Convert(120007));
			HtmlEntityService.AddSingle(dictionary, "rsh;", HtmlEntityService.Convert(8625));
			HtmlEntityService.AddSingle(dictionary, "rsqb;", HtmlEntityService.Convert(93));
			HtmlEntityService.AddSingle(dictionary, "rsquo;", HtmlEntityService.Convert(8217));
			HtmlEntityService.AddSingle(dictionary, "rsquor;", HtmlEntityService.Convert(8217));
			HtmlEntityService.AddSingle(dictionary, "rthree;", HtmlEntityService.Convert(8908));
			HtmlEntityService.AddSingle(dictionary, "rtimes;", HtmlEntityService.Convert(8906));
			HtmlEntityService.AddSingle(dictionary, "rtri;", HtmlEntityService.Convert(9657));
			HtmlEntityService.AddSingle(dictionary, "rtrie;", HtmlEntityService.Convert(8885));
			HtmlEntityService.AddSingle(dictionary, "rtrif;", HtmlEntityService.Convert(9656));
			HtmlEntityService.AddSingle(dictionary, "rtriltri;", HtmlEntityService.Convert(10702));
			HtmlEntityService.AddSingle(dictionary, "ruluhar;", HtmlEntityService.Convert(10600));
			HtmlEntityService.AddSingle(dictionary, "rx;", HtmlEntityService.Convert(8478));
			return dictionary;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0002921C File Offset: 0x0002741C
		private Dictionary<string, string> GetSymbolBigR()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Racute;", HtmlEntityService.Convert(340));
			HtmlEntityService.AddSingle(dictionary, "Rang;", HtmlEntityService.Convert(10219));
			HtmlEntityService.AddSingle(dictionary, "Rarr;", HtmlEntityService.Convert(8608));
			HtmlEntityService.AddSingle(dictionary, "Rarrtl;", HtmlEntityService.Convert(10518));
			HtmlEntityService.AddSingle(dictionary, "RBarr;", HtmlEntityService.Convert(10512));
			HtmlEntityService.AddSingle(dictionary, "Rcaron;", HtmlEntityService.Convert(344));
			HtmlEntityService.AddSingle(dictionary, "Rcedil;", HtmlEntityService.Convert(342));
			HtmlEntityService.AddSingle(dictionary, "Rcy;", HtmlEntityService.Convert(1056));
			HtmlEntityService.AddSingle(dictionary, "Re;", HtmlEntityService.Convert(8476));
			HtmlEntityService.AddBoth(dictionary, "REG;", HtmlEntityService.Convert(174));
			HtmlEntityService.AddSingle(dictionary, "ReverseElement;", HtmlEntityService.Convert(8715));
			HtmlEntityService.AddSingle(dictionary, "ReverseEquilibrium;", HtmlEntityService.Convert(8651));
			HtmlEntityService.AddSingle(dictionary, "ReverseUpEquilibrium;", HtmlEntityService.Convert(10607));
			HtmlEntityService.AddSingle(dictionary, "Rfr;", HtmlEntityService.Convert(8476));
			HtmlEntityService.AddSingle(dictionary, "Rho;", HtmlEntityService.Convert(929));
			HtmlEntityService.AddSingle(dictionary, "RightAngleBracket;", HtmlEntityService.Convert(10217));
			HtmlEntityService.AddSingle(dictionary, "RightArrow;", HtmlEntityService.Convert(8594));
			HtmlEntityService.AddSingle(dictionary, "Rightarrow;", HtmlEntityService.Convert(8658));
			HtmlEntityService.AddSingle(dictionary, "RightArrowBar;", HtmlEntityService.Convert(8677));
			HtmlEntityService.AddSingle(dictionary, "RightArrowLeftArrow;", HtmlEntityService.Convert(8644));
			HtmlEntityService.AddSingle(dictionary, "RightCeiling;", HtmlEntityService.Convert(8969));
			HtmlEntityService.AddSingle(dictionary, "RightDoubleBracket;", HtmlEntityService.Convert(10215));
			HtmlEntityService.AddSingle(dictionary, "RightDownTeeVector;", HtmlEntityService.Convert(10589));
			HtmlEntityService.AddSingle(dictionary, "RightDownVector;", HtmlEntityService.Convert(8642));
			HtmlEntityService.AddSingle(dictionary, "RightDownVectorBar;", HtmlEntityService.Convert(10581));
			HtmlEntityService.AddSingle(dictionary, "RightFloor;", HtmlEntityService.Convert(8971));
			HtmlEntityService.AddSingle(dictionary, "RightTee;", HtmlEntityService.Convert(8866));
			HtmlEntityService.AddSingle(dictionary, "RightTeeArrow;", HtmlEntityService.Convert(8614));
			HtmlEntityService.AddSingle(dictionary, "RightTeeVector;", HtmlEntityService.Convert(10587));
			HtmlEntityService.AddSingle(dictionary, "RightTriangle;", HtmlEntityService.Convert(8883));
			HtmlEntityService.AddSingle(dictionary, "RightTriangleBar;", HtmlEntityService.Convert(10704));
			HtmlEntityService.AddSingle(dictionary, "RightTriangleEqual;", HtmlEntityService.Convert(8885));
			HtmlEntityService.AddSingle(dictionary, "RightUpDownVector;", HtmlEntityService.Convert(10575));
			HtmlEntityService.AddSingle(dictionary, "RightUpTeeVector;", HtmlEntityService.Convert(10588));
			HtmlEntityService.AddSingle(dictionary, "RightUpVector;", HtmlEntityService.Convert(8638));
			HtmlEntityService.AddSingle(dictionary, "RightUpVectorBar;", HtmlEntityService.Convert(10580));
			HtmlEntityService.AddSingle(dictionary, "RightVector;", HtmlEntityService.Convert(8640));
			HtmlEntityService.AddSingle(dictionary, "RightVectorBar;", HtmlEntityService.Convert(10579));
			HtmlEntityService.AddSingle(dictionary, "Ropf;", HtmlEntityService.Convert(8477));
			HtmlEntityService.AddSingle(dictionary, "RoundImplies;", HtmlEntityService.Convert(10608));
			HtmlEntityService.AddSingle(dictionary, "Rrightarrow;", HtmlEntityService.Convert(8667));
			HtmlEntityService.AddSingle(dictionary, "Rscr;", HtmlEntityService.Convert(8475));
			HtmlEntityService.AddSingle(dictionary, "Rsh;", HtmlEntityService.Convert(8625));
			HtmlEntityService.AddSingle(dictionary, "RuleDelayed;", HtmlEntityService.Convert(10740));
			return dictionary;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000295CC File Offset: 0x000277CC
		private Dictionary<string, string> GetSymbolLittleS()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "sacute;", HtmlEntityService.Convert(347));
			HtmlEntityService.AddSingle(dictionary, "sbquo;", HtmlEntityService.Convert(8218));
			HtmlEntityService.AddSingle(dictionary, "sc;", HtmlEntityService.Convert(8827));
			HtmlEntityService.AddSingle(dictionary, "scap;", HtmlEntityService.Convert(10936));
			HtmlEntityService.AddSingle(dictionary, "scaron;", HtmlEntityService.Convert(353));
			HtmlEntityService.AddSingle(dictionary, "sccue;", HtmlEntityService.Convert(8829));
			HtmlEntityService.AddSingle(dictionary, "scE;", HtmlEntityService.Convert(10932));
			HtmlEntityService.AddSingle(dictionary, "sce;", HtmlEntityService.Convert(10928));
			HtmlEntityService.AddSingle(dictionary, "scedil;", HtmlEntityService.Convert(351));
			HtmlEntityService.AddSingle(dictionary, "scirc;", HtmlEntityService.Convert(349));
			HtmlEntityService.AddSingle(dictionary, "scnap;", HtmlEntityService.Convert(10938));
			HtmlEntityService.AddSingle(dictionary, "scnE;", HtmlEntityService.Convert(10934));
			HtmlEntityService.AddSingle(dictionary, "scnsim;", HtmlEntityService.Convert(8937));
			HtmlEntityService.AddSingle(dictionary, "scpolint;", HtmlEntityService.Convert(10771));
			HtmlEntityService.AddSingle(dictionary, "scsim;", HtmlEntityService.Convert(8831));
			HtmlEntityService.AddSingle(dictionary, "scy;", HtmlEntityService.Convert(1089));
			HtmlEntityService.AddSingle(dictionary, "sdot;", HtmlEntityService.Convert(8901));
			HtmlEntityService.AddSingle(dictionary, "sdotb;", HtmlEntityService.Convert(8865));
			HtmlEntityService.AddSingle(dictionary, "sdote;", HtmlEntityService.Convert(10854));
			HtmlEntityService.AddSingle(dictionary, "searhk;", HtmlEntityService.Convert(10533));
			HtmlEntityService.AddSingle(dictionary, "seArr;", HtmlEntityService.Convert(8664));
			HtmlEntityService.AddSingle(dictionary, "searr;", HtmlEntityService.Convert(8600));
			HtmlEntityService.AddSingle(dictionary, "searrow;", HtmlEntityService.Convert(8600));
			HtmlEntityService.AddBoth(dictionary, "sect;", HtmlEntityService.Convert(167));
			HtmlEntityService.AddSingle(dictionary, "semi;", HtmlEntityService.Convert(59));
			HtmlEntityService.AddSingle(dictionary, "seswar;", HtmlEntityService.Convert(10537));
			HtmlEntityService.AddSingle(dictionary, "setminus;", HtmlEntityService.Convert(8726));
			HtmlEntityService.AddSingle(dictionary, "setmn;", HtmlEntityService.Convert(8726));
			HtmlEntityService.AddSingle(dictionary, "sext;", HtmlEntityService.Convert(10038));
			HtmlEntityService.AddSingle(dictionary, "sfr;", HtmlEntityService.Convert(120112));
			HtmlEntityService.AddSingle(dictionary, "sfrown;", HtmlEntityService.Convert(8994));
			HtmlEntityService.AddSingle(dictionary, "sharp;", HtmlEntityService.Convert(9839));
			HtmlEntityService.AddSingle(dictionary, "shchcy;", HtmlEntityService.Convert(1097));
			HtmlEntityService.AddSingle(dictionary, "shcy;", HtmlEntityService.Convert(1096));
			HtmlEntityService.AddSingle(dictionary, "shortmid;", HtmlEntityService.Convert(8739));
			HtmlEntityService.AddSingle(dictionary, "shortparallel;", HtmlEntityService.Convert(8741));
			HtmlEntityService.AddBoth(dictionary, "shy;", HtmlEntityService.Convert(173));
			HtmlEntityService.AddSingle(dictionary, "sigma;", HtmlEntityService.Convert(963));
			HtmlEntityService.AddSingle(dictionary, "sigmaf;", HtmlEntityService.Convert(962));
			HtmlEntityService.AddSingle(dictionary, "sigmav;", HtmlEntityService.Convert(962));
			HtmlEntityService.AddSingle(dictionary, "sim;", HtmlEntityService.Convert(8764));
			HtmlEntityService.AddSingle(dictionary, "simdot;", HtmlEntityService.Convert(10858));
			HtmlEntityService.AddSingle(dictionary, "sime;", HtmlEntityService.Convert(8771));
			HtmlEntityService.AddSingle(dictionary, "simeq;", HtmlEntityService.Convert(8771));
			HtmlEntityService.AddSingle(dictionary, "simg;", HtmlEntityService.Convert(10910));
			HtmlEntityService.AddSingle(dictionary, "simgE;", HtmlEntityService.Convert(10912));
			HtmlEntityService.AddSingle(dictionary, "siml;", HtmlEntityService.Convert(10909));
			HtmlEntityService.AddSingle(dictionary, "simlE;", HtmlEntityService.Convert(10911));
			HtmlEntityService.AddSingle(dictionary, "simne;", HtmlEntityService.Convert(8774));
			HtmlEntityService.AddSingle(dictionary, "simplus;", HtmlEntityService.Convert(10788));
			HtmlEntityService.AddSingle(dictionary, "simrarr;", HtmlEntityService.Convert(10610));
			HtmlEntityService.AddSingle(dictionary, "slarr;", HtmlEntityService.Convert(8592));
			HtmlEntityService.AddSingle(dictionary, "smallsetminus;", HtmlEntityService.Convert(8726));
			HtmlEntityService.AddSingle(dictionary, "smashp;", HtmlEntityService.Convert(10803));
			HtmlEntityService.AddSingle(dictionary, "smeparsl;", HtmlEntityService.Convert(10724));
			HtmlEntityService.AddSingle(dictionary, "smid;", HtmlEntityService.Convert(8739));
			HtmlEntityService.AddSingle(dictionary, "smile;", HtmlEntityService.Convert(8995));
			HtmlEntityService.AddSingle(dictionary, "smt;", HtmlEntityService.Convert(10922));
			HtmlEntityService.AddSingle(dictionary, "smte;", HtmlEntityService.Convert(10924));
			HtmlEntityService.AddSingle(dictionary, "smtes;", HtmlEntityService.Convert(10924, 65024));
			HtmlEntityService.AddSingle(dictionary, "softcy;", HtmlEntityService.Convert(1100));
			HtmlEntityService.AddSingle(dictionary, "sol;", HtmlEntityService.Convert(47));
			HtmlEntityService.AddSingle(dictionary, "solb;", HtmlEntityService.Convert(10692));
			HtmlEntityService.AddSingle(dictionary, "solbar;", HtmlEntityService.Convert(9023));
			HtmlEntityService.AddSingle(dictionary, "sopf;", HtmlEntityService.Convert(120164));
			HtmlEntityService.AddSingle(dictionary, "spades;", HtmlEntityService.Convert(9824));
			HtmlEntityService.AddSingle(dictionary, "spadesuit;", HtmlEntityService.Convert(9824));
			HtmlEntityService.AddSingle(dictionary, "spar;", HtmlEntityService.Convert(8741));
			HtmlEntityService.AddSingle(dictionary, "sqcap;", HtmlEntityService.Convert(8851));
			HtmlEntityService.AddSingle(dictionary, "sqcaps;", HtmlEntityService.Convert(8851, 65024));
			HtmlEntityService.AddSingle(dictionary, "sqcup;", HtmlEntityService.Convert(8852));
			HtmlEntityService.AddSingle(dictionary, "sqcups;", HtmlEntityService.Convert(8852, 65024));
			HtmlEntityService.AddSingle(dictionary, "sqsub;", HtmlEntityService.Convert(8847));
			HtmlEntityService.AddSingle(dictionary, "sqsube;", HtmlEntityService.Convert(8849));
			HtmlEntityService.AddSingle(dictionary, "sqsubset;", HtmlEntityService.Convert(8847));
			HtmlEntityService.AddSingle(dictionary, "sqsubseteq;", HtmlEntityService.Convert(8849));
			HtmlEntityService.AddSingle(dictionary, "sqsup;", HtmlEntityService.Convert(8848));
			HtmlEntityService.AddSingle(dictionary, "sqsupe;", HtmlEntityService.Convert(8850));
			HtmlEntityService.AddSingle(dictionary, "sqsupset;", HtmlEntityService.Convert(8848));
			HtmlEntityService.AddSingle(dictionary, "sqsupseteq;", HtmlEntityService.Convert(8850));
			HtmlEntityService.AddSingle(dictionary, "squ;", HtmlEntityService.Convert(9633));
			HtmlEntityService.AddSingle(dictionary, "square;", HtmlEntityService.Convert(9633));
			HtmlEntityService.AddSingle(dictionary, "squarf;", HtmlEntityService.Convert(9642));
			HtmlEntityService.AddSingle(dictionary, "squf;", HtmlEntityService.Convert(9642));
			HtmlEntityService.AddSingle(dictionary, "srarr;", HtmlEntityService.Convert(8594));
			HtmlEntityService.AddSingle(dictionary, "sscr;", HtmlEntityService.Convert(120008));
			HtmlEntityService.AddSingle(dictionary, "ssetmn;", HtmlEntityService.Convert(8726));
			HtmlEntityService.AddSingle(dictionary, "ssmile;", HtmlEntityService.Convert(8995));
			HtmlEntityService.AddSingle(dictionary, "sstarf;", HtmlEntityService.Convert(8902));
			HtmlEntityService.AddSingle(dictionary, "star;", HtmlEntityService.Convert(9734));
			HtmlEntityService.AddSingle(dictionary, "starf;", HtmlEntityService.Convert(9733));
			HtmlEntityService.AddSingle(dictionary, "straightepsilon;", HtmlEntityService.Convert(1013));
			HtmlEntityService.AddSingle(dictionary, "straightphi;", HtmlEntityService.Convert(981));
			HtmlEntityService.AddSingle(dictionary, "strns;", HtmlEntityService.Convert(175));
			HtmlEntityService.AddSingle(dictionary, "sub;", HtmlEntityService.Convert(8834));
			HtmlEntityService.AddSingle(dictionary, "subdot;", HtmlEntityService.Convert(10941));
			HtmlEntityService.AddSingle(dictionary, "subE;", HtmlEntityService.Convert(10949));
			HtmlEntityService.AddSingle(dictionary, "sube;", HtmlEntityService.Convert(8838));
			HtmlEntityService.AddSingle(dictionary, "subedot;", HtmlEntityService.Convert(10947));
			HtmlEntityService.AddSingle(dictionary, "submult;", HtmlEntityService.Convert(10945));
			HtmlEntityService.AddSingle(dictionary, "subnE;", HtmlEntityService.Convert(10955));
			HtmlEntityService.AddSingle(dictionary, "subne;", HtmlEntityService.Convert(8842));
			HtmlEntityService.AddSingle(dictionary, "subplus;", HtmlEntityService.Convert(10943));
			HtmlEntityService.AddSingle(dictionary, "subrarr;", HtmlEntityService.Convert(10617));
			HtmlEntityService.AddSingle(dictionary, "subset;", HtmlEntityService.Convert(8834));
			HtmlEntityService.AddSingle(dictionary, "subseteq;", HtmlEntityService.Convert(8838));
			HtmlEntityService.AddSingle(dictionary, "subseteqq;", HtmlEntityService.Convert(10949));
			HtmlEntityService.AddSingle(dictionary, "subsetneq;", HtmlEntityService.Convert(8842));
			HtmlEntityService.AddSingle(dictionary, "subsetneqq;", HtmlEntityService.Convert(10955));
			HtmlEntityService.AddSingle(dictionary, "subsim;", HtmlEntityService.Convert(10951));
			HtmlEntityService.AddSingle(dictionary, "subsub;", HtmlEntityService.Convert(10965));
			HtmlEntityService.AddSingle(dictionary, "subsup;", HtmlEntityService.Convert(10963));
			HtmlEntityService.AddSingle(dictionary, "succ;", HtmlEntityService.Convert(8827));
			HtmlEntityService.AddSingle(dictionary, "succapprox;", HtmlEntityService.Convert(10936));
			HtmlEntityService.AddSingle(dictionary, "succcurlyeq;", HtmlEntityService.Convert(8829));
			HtmlEntityService.AddSingle(dictionary, "succeq;", HtmlEntityService.Convert(10928));
			HtmlEntityService.AddSingle(dictionary, "succnapprox;", HtmlEntityService.Convert(10938));
			HtmlEntityService.AddSingle(dictionary, "succneqq;", HtmlEntityService.Convert(10934));
			HtmlEntityService.AddSingle(dictionary, "succnsim;", HtmlEntityService.Convert(8937));
			HtmlEntityService.AddSingle(dictionary, "succsim;", HtmlEntityService.Convert(8831));
			HtmlEntityService.AddSingle(dictionary, "sum;", HtmlEntityService.Convert(8721));
			HtmlEntityService.AddSingle(dictionary, "sung;", HtmlEntityService.Convert(9834));
			HtmlEntityService.AddSingle(dictionary, "sup;", HtmlEntityService.Convert(8835));
			HtmlEntityService.AddBoth(dictionary, "sup1;", HtmlEntityService.Convert(185));
			HtmlEntityService.AddBoth(dictionary, "sup2;", HtmlEntityService.Convert(178));
			HtmlEntityService.AddBoth(dictionary, "sup3;", HtmlEntityService.Convert(179));
			HtmlEntityService.AddSingle(dictionary, "supdot;", HtmlEntityService.Convert(10942));
			HtmlEntityService.AddSingle(dictionary, "supdsub;", HtmlEntityService.Convert(10968));
			HtmlEntityService.AddSingle(dictionary, "supE;", HtmlEntityService.Convert(10950));
			HtmlEntityService.AddSingle(dictionary, "supe;", HtmlEntityService.Convert(8839));
			HtmlEntityService.AddSingle(dictionary, "supedot;", HtmlEntityService.Convert(10948));
			HtmlEntityService.AddSingle(dictionary, "suphsol;", HtmlEntityService.Convert(10185));
			HtmlEntityService.AddSingle(dictionary, "suphsub;", HtmlEntityService.Convert(10967));
			HtmlEntityService.AddSingle(dictionary, "suplarr;", HtmlEntityService.Convert(10619));
			HtmlEntityService.AddSingle(dictionary, "supmult;", HtmlEntityService.Convert(10946));
			HtmlEntityService.AddSingle(dictionary, "supnE;", HtmlEntityService.Convert(10956));
			HtmlEntityService.AddSingle(dictionary, "supne;", HtmlEntityService.Convert(8843));
			HtmlEntityService.AddSingle(dictionary, "supplus;", HtmlEntityService.Convert(10944));
			HtmlEntityService.AddSingle(dictionary, "supset;", HtmlEntityService.Convert(8835));
			HtmlEntityService.AddSingle(dictionary, "supseteq;", HtmlEntityService.Convert(8839));
			HtmlEntityService.AddSingle(dictionary, "supseteqq;", HtmlEntityService.Convert(10950));
			HtmlEntityService.AddSingle(dictionary, "supsetneq;", HtmlEntityService.Convert(8843));
			HtmlEntityService.AddSingle(dictionary, "supsetneqq;", HtmlEntityService.Convert(10956));
			HtmlEntityService.AddSingle(dictionary, "supsim;", HtmlEntityService.Convert(10952));
			HtmlEntityService.AddSingle(dictionary, "supsub;", HtmlEntityService.Convert(10964));
			HtmlEntityService.AddSingle(dictionary, "supsup;", HtmlEntityService.Convert(10966));
			HtmlEntityService.AddSingle(dictionary, "swarhk;", HtmlEntityService.Convert(10534));
			HtmlEntityService.AddSingle(dictionary, "swArr;", HtmlEntityService.Convert(8665));
			HtmlEntityService.AddSingle(dictionary, "swarr;", HtmlEntityService.Convert(8601));
			HtmlEntityService.AddSingle(dictionary, "swarrow;", HtmlEntityService.Convert(8601));
			HtmlEntityService.AddSingle(dictionary, "swnwar;", HtmlEntityService.Convert(10538));
			HtmlEntityService.AddBoth(dictionary, "szlig;", HtmlEntityService.Convert(223));
			return dictionary;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0002A260 File Offset: 0x00028460
		private Dictionary<string, string> GetSymbolBigS()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Sacute;", HtmlEntityService.Convert(346));
			HtmlEntityService.AddSingle(dictionary, "Sc;", HtmlEntityService.Convert(10940));
			HtmlEntityService.AddSingle(dictionary, "Scaron;", HtmlEntityService.Convert(352));
			HtmlEntityService.AddSingle(dictionary, "Scedil;", HtmlEntityService.Convert(350));
			HtmlEntityService.AddSingle(dictionary, "Scirc;", HtmlEntityService.Convert(348));
			HtmlEntityService.AddSingle(dictionary, "Scy;", HtmlEntityService.Convert(1057));
			HtmlEntityService.AddSingle(dictionary, "Sfr;", HtmlEntityService.Convert(120086));
			HtmlEntityService.AddSingle(dictionary, "SHCHcy;", HtmlEntityService.Convert(1065));
			HtmlEntityService.AddSingle(dictionary, "SHcy;", HtmlEntityService.Convert(1064));
			HtmlEntityService.AddSingle(dictionary, "ShortDownArrow;", HtmlEntityService.Convert(8595));
			HtmlEntityService.AddSingle(dictionary, "ShortLeftArrow;", HtmlEntityService.Convert(8592));
			HtmlEntityService.AddSingle(dictionary, "ShortRightArrow;", HtmlEntityService.Convert(8594));
			HtmlEntityService.AddSingle(dictionary, "ShortUpArrow;", HtmlEntityService.Convert(8593));
			HtmlEntityService.AddSingle(dictionary, "Sigma;", HtmlEntityService.Convert(931));
			HtmlEntityService.AddSingle(dictionary, "SmallCircle;", HtmlEntityService.Convert(8728));
			HtmlEntityService.AddSingle(dictionary, "SOFTcy;", HtmlEntityService.Convert(1068));
			HtmlEntityService.AddSingle(dictionary, "Sopf;", HtmlEntityService.Convert(120138));
			HtmlEntityService.AddSingle(dictionary, "Sqrt;", HtmlEntityService.Convert(8730));
			HtmlEntityService.AddSingle(dictionary, "Square;", HtmlEntityService.Convert(9633));
			HtmlEntityService.AddSingle(dictionary, "SquareIntersection;", HtmlEntityService.Convert(8851));
			HtmlEntityService.AddSingle(dictionary, "SquareSubset;", HtmlEntityService.Convert(8847));
			HtmlEntityService.AddSingle(dictionary, "SquareSubsetEqual;", HtmlEntityService.Convert(8849));
			HtmlEntityService.AddSingle(dictionary, "SquareSuperset;", HtmlEntityService.Convert(8848));
			HtmlEntityService.AddSingle(dictionary, "SquareSupersetEqual;", HtmlEntityService.Convert(8850));
			HtmlEntityService.AddSingle(dictionary, "SquareUnion;", HtmlEntityService.Convert(8852));
			HtmlEntityService.AddSingle(dictionary, "Sscr;", HtmlEntityService.Convert(119982));
			HtmlEntityService.AddSingle(dictionary, "Star;", HtmlEntityService.Convert(8902));
			HtmlEntityService.AddSingle(dictionary, "Sub;", HtmlEntityService.Convert(8912));
			HtmlEntityService.AddSingle(dictionary, "Subset;", HtmlEntityService.Convert(8912));
			HtmlEntityService.AddSingle(dictionary, "SubsetEqual;", HtmlEntityService.Convert(8838));
			HtmlEntityService.AddSingle(dictionary, "Succeeds;", HtmlEntityService.Convert(8827));
			HtmlEntityService.AddSingle(dictionary, "SucceedsEqual;", HtmlEntityService.Convert(10928));
			HtmlEntityService.AddSingle(dictionary, "SucceedsSlantEqual;", HtmlEntityService.Convert(8829));
			HtmlEntityService.AddSingle(dictionary, "SucceedsTilde;", HtmlEntityService.Convert(8831));
			HtmlEntityService.AddSingle(dictionary, "SuchThat;", HtmlEntityService.Convert(8715));
			HtmlEntityService.AddSingle(dictionary, "Sum;", HtmlEntityService.Convert(8721));
			HtmlEntityService.AddSingle(dictionary, "Sup;", HtmlEntityService.Convert(8913));
			HtmlEntityService.AddSingle(dictionary, "Superset;", HtmlEntityService.Convert(8835));
			HtmlEntityService.AddSingle(dictionary, "SupersetEqual;", HtmlEntityService.Convert(8839));
			HtmlEntityService.AddSingle(dictionary, "Supset;", HtmlEntityService.Convert(8913));
			return dictionary;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002A5BC File Offset: 0x000287BC
		private Dictionary<string, string> GetSymbolLittleT()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "target;", HtmlEntityService.Convert(8982));
			HtmlEntityService.AddSingle(dictionary, "tau;", HtmlEntityService.Convert(964));
			HtmlEntityService.AddSingle(dictionary, "tbrk;", HtmlEntityService.Convert(9140));
			HtmlEntityService.AddSingle(dictionary, "tcaron;", HtmlEntityService.Convert(357));
			HtmlEntityService.AddSingle(dictionary, "tcedil;", HtmlEntityService.Convert(355));
			HtmlEntityService.AddSingle(dictionary, "tcy;", HtmlEntityService.Convert(1090));
			HtmlEntityService.AddSingle(dictionary, "tdot;", HtmlEntityService.Convert(8411));
			HtmlEntityService.AddSingle(dictionary, "telrec;", HtmlEntityService.Convert(8981));
			HtmlEntityService.AddSingle(dictionary, "tfr;", HtmlEntityService.Convert(120113));
			HtmlEntityService.AddSingle(dictionary, "there4;", HtmlEntityService.Convert(8756));
			HtmlEntityService.AddSingle(dictionary, "therefore;", HtmlEntityService.Convert(8756));
			HtmlEntityService.AddSingle(dictionary, "theta;", HtmlEntityService.Convert(952));
			HtmlEntityService.AddSingle(dictionary, "thetasym;", HtmlEntityService.Convert(977));
			HtmlEntityService.AddSingle(dictionary, "thetav;", HtmlEntityService.Convert(977));
			HtmlEntityService.AddSingle(dictionary, "thickapprox;", HtmlEntityService.Convert(8776));
			HtmlEntityService.AddSingle(dictionary, "thicksim;", HtmlEntityService.Convert(8764));
			HtmlEntityService.AddSingle(dictionary, "thinsp;", HtmlEntityService.Convert(8201));
			HtmlEntityService.AddSingle(dictionary, "thkap;", HtmlEntityService.Convert(8776));
			HtmlEntityService.AddSingle(dictionary, "thksim;", HtmlEntityService.Convert(8764));
			HtmlEntityService.AddBoth(dictionary, "thorn;", HtmlEntityService.Convert(254));
			HtmlEntityService.AddSingle(dictionary, "tilde;", HtmlEntityService.Convert(732));
			HtmlEntityService.AddBoth(dictionary, "times;", HtmlEntityService.Convert(215));
			HtmlEntityService.AddSingle(dictionary, "timesb;", HtmlEntityService.Convert(8864));
			HtmlEntityService.AddSingle(dictionary, "timesbar;", HtmlEntityService.Convert(10801));
			HtmlEntityService.AddSingle(dictionary, "timesd;", HtmlEntityService.Convert(10800));
			HtmlEntityService.AddSingle(dictionary, "tint;", HtmlEntityService.Convert(8749));
			HtmlEntityService.AddSingle(dictionary, "toea;", HtmlEntityService.Convert(10536));
			HtmlEntityService.AddSingle(dictionary, "top;", HtmlEntityService.Convert(8868));
			HtmlEntityService.AddSingle(dictionary, "topbot;", HtmlEntityService.Convert(9014));
			HtmlEntityService.AddSingle(dictionary, "topcir;", HtmlEntityService.Convert(10993));
			HtmlEntityService.AddSingle(dictionary, "topf;", HtmlEntityService.Convert(120165));
			HtmlEntityService.AddSingle(dictionary, "topfork;", HtmlEntityService.Convert(10970));
			HtmlEntityService.AddSingle(dictionary, "tosa;", HtmlEntityService.Convert(10537));
			HtmlEntityService.AddSingle(dictionary, "tprime;", HtmlEntityService.Convert(8244));
			HtmlEntityService.AddSingle(dictionary, "trade;", HtmlEntityService.Convert(8482));
			HtmlEntityService.AddSingle(dictionary, "triangle;", HtmlEntityService.Convert(9653));
			HtmlEntityService.AddSingle(dictionary, "triangledown;", HtmlEntityService.Convert(9663));
			HtmlEntityService.AddSingle(dictionary, "triangleleft;", HtmlEntityService.Convert(9667));
			HtmlEntityService.AddSingle(dictionary, "trianglelefteq;", HtmlEntityService.Convert(8884));
			HtmlEntityService.AddSingle(dictionary, "triangleq;", HtmlEntityService.Convert(8796));
			HtmlEntityService.AddSingle(dictionary, "triangleright;", HtmlEntityService.Convert(9657));
			HtmlEntityService.AddSingle(dictionary, "trianglerighteq;", HtmlEntityService.Convert(8885));
			HtmlEntityService.AddSingle(dictionary, "tridot;", HtmlEntityService.Convert(9708));
			HtmlEntityService.AddSingle(dictionary, "trie;", HtmlEntityService.Convert(8796));
			HtmlEntityService.AddSingle(dictionary, "triminus;", HtmlEntityService.Convert(10810));
			HtmlEntityService.AddSingle(dictionary, "triplus;", HtmlEntityService.Convert(10809));
			HtmlEntityService.AddSingle(dictionary, "trisb;", HtmlEntityService.Convert(10701));
			HtmlEntityService.AddSingle(dictionary, "tritime;", HtmlEntityService.Convert(10811));
			HtmlEntityService.AddSingle(dictionary, "trpezium;", HtmlEntityService.Convert(9186));
			HtmlEntityService.AddSingle(dictionary, "tscr;", HtmlEntityService.Convert(120009));
			HtmlEntityService.AddSingle(dictionary, "tscy;", HtmlEntityService.Convert(1094));
			HtmlEntityService.AddSingle(dictionary, "tshcy;", HtmlEntityService.Convert(1115));
			HtmlEntityService.AddSingle(dictionary, "tstrok;", HtmlEntityService.Convert(359));
			HtmlEntityService.AddSingle(dictionary, "twixt;", HtmlEntityService.Convert(8812));
			HtmlEntityService.AddSingle(dictionary, "twoheadleftarrow;", HtmlEntityService.Convert(8606));
			HtmlEntityService.AddSingle(dictionary, "twoheadrightarrow;", HtmlEntityService.Convert(8608));
			return dictionary;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002AA68 File Offset: 0x00028C68
		private Dictionary<string, string> GetSymbolBigT()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Tab;", HtmlEntityService.Convert(9));
			HtmlEntityService.AddSingle(dictionary, "Tau;", HtmlEntityService.Convert(932));
			HtmlEntityService.AddSingle(dictionary, "Tcaron;", HtmlEntityService.Convert(356));
			HtmlEntityService.AddSingle(dictionary, "Tcedil;", HtmlEntityService.Convert(354));
			HtmlEntityService.AddSingle(dictionary, "Tcy;", HtmlEntityService.Convert(1058));
			HtmlEntityService.AddSingle(dictionary, "Tfr;", HtmlEntityService.Convert(120087));
			HtmlEntityService.AddSingle(dictionary, "Therefore;", HtmlEntityService.Convert(8756));
			HtmlEntityService.AddSingle(dictionary, "Theta;", HtmlEntityService.Convert(920));
			HtmlEntityService.AddSingle(dictionary, "ThickSpace;", HtmlEntityService.Convert(8287, 8202));
			HtmlEntityService.AddSingle(dictionary, "ThinSpace;", HtmlEntityService.Convert(8201));
			HtmlEntityService.AddBoth(dictionary, "THORN;", HtmlEntityService.Convert(222));
			HtmlEntityService.AddSingle(dictionary, "Tilde;", HtmlEntityService.Convert(8764));
			HtmlEntityService.AddSingle(dictionary, "TildeEqual;", HtmlEntityService.Convert(8771));
			HtmlEntityService.AddSingle(dictionary, "TildeFullEqual;", HtmlEntityService.Convert(8773));
			HtmlEntityService.AddSingle(dictionary, "TildeTilde;", HtmlEntityService.Convert(8776));
			HtmlEntityService.AddSingle(dictionary, "Topf;", HtmlEntityService.Convert(120139));
			HtmlEntityService.AddSingle(dictionary, "TRADE;", HtmlEntityService.Convert(8482));
			HtmlEntityService.AddSingle(dictionary, "TripleDot;", HtmlEntityService.Convert(8411));
			HtmlEntityService.AddSingle(dictionary, "Tscr;", HtmlEntityService.Convert(119983));
			HtmlEntityService.AddSingle(dictionary, "TScy;", HtmlEntityService.Convert(1062));
			HtmlEntityService.AddSingle(dictionary, "TSHcy;", HtmlEntityService.Convert(1035));
			HtmlEntityService.AddSingle(dictionary, "Tstrok;", HtmlEntityService.Convert(358));
			return dictionary;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002AC4C File Offset: 0x00028E4C
		private Dictionary<string, string> GetSymbolLittleU()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "uacute;", HtmlEntityService.Convert(250));
			HtmlEntityService.AddSingle(dictionary, "uArr;", HtmlEntityService.Convert(8657));
			HtmlEntityService.AddSingle(dictionary, "uarr;", HtmlEntityService.Convert(8593));
			HtmlEntityService.AddSingle(dictionary, "ubrcy;", HtmlEntityService.Convert(1118));
			HtmlEntityService.AddSingle(dictionary, "ubreve;", HtmlEntityService.Convert(365));
			HtmlEntityService.AddBoth(dictionary, "ucirc;", HtmlEntityService.Convert(251));
			HtmlEntityService.AddSingle(dictionary, "ucy;", HtmlEntityService.Convert(1091));
			HtmlEntityService.AddSingle(dictionary, "udarr;", HtmlEntityService.Convert(8645));
			HtmlEntityService.AddSingle(dictionary, "udblac;", HtmlEntityService.Convert(369));
			HtmlEntityService.AddSingle(dictionary, "udhar;", HtmlEntityService.Convert(10606));
			HtmlEntityService.AddSingle(dictionary, "ufisht;", HtmlEntityService.Convert(10622));
			HtmlEntityService.AddSingle(dictionary, "ufr;", HtmlEntityService.Convert(120114));
			HtmlEntityService.AddBoth(dictionary, "ugrave;", HtmlEntityService.Convert(249));
			HtmlEntityService.AddSingle(dictionary, "uHar;", HtmlEntityService.Convert(10595));
			HtmlEntityService.AddSingle(dictionary, "uharl;", HtmlEntityService.Convert(8639));
			HtmlEntityService.AddSingle(dictionary, "uharr;", HtmlEntityService.Convert(8638));
			HtmlEntityService.AddSingle(dictionary, "uhblk;", HtmlEntityService.Convert(9600));
			HtmlEntityService.AddSingle(dictionary, "ulcorn;", HtmlEntityService.Convert(8988));
			HtmlEntityService.AddSingle(dictionary, "ulcorner;", HtmlEntityService.Convert(8988));
			HtmlEntityService.AddSingle(dictionary, "ulcrop;", HtmlEntityService.Convert(8975));
			HtmlEntityService.AddSingle(dictionary, "ultri;", HtmlEntityService.Convert(9720));
			HtmlEntityService.AddSingle(dictionary, "umacr;", HtmlEntityService.Convert(363));
			HtmlEntityService.AddBoth(dictionary, "uml;", HtmlEntityService.Convert(168));
			HtmlEntityService.AddSingle(dictionary, "uogon;", HtmlEntityService.Convert(371));
			HtmlEntityService.AddSingle(dictionary, "uopf;", HtmlEntityService.Convert(120166));
			HtmlEntityService.AddSingle(dictionary, "uparrow;", HtmlEntityService.Convert(8593));
			HtmlEntityService.AddSingle(dictionary, "updownarrow;", HtmlEntityService.Convert(8597));
			HtmlEntityService.AddSingle(dictionary, "upharpoonleft;", HtmlEntityService.Convert(8639));
			HtmlEntityService.AddSingle(dictionary, "upharpoonright;", HtmlEntityService.Convert(8638));
			HtmlEntityService.AddSingle(dictionary, "uplus;", HtmlEntityService.Convert(8846));
			HtmlEntityService.AddSingle(dictionary, "upsi;", HtmlEntityService.Convert(965));
			HtmlEntityService.AddSingle(dictionary, "upsih;", HtmlEntityService.Convert(978));
			HtmlEntityService.AddSingle(dictionary, "upsilon;", HtmlEntityService.Convert(965));
			HtmlEntityService.AddSingle(dictionary, "upuparrows;", HtmlEntityService.Convert(8648));
			HtmlEntityService.AddSingle(dictionary, "urcorn;", HtmlEntityService.Convert(8989));
			HtmlEntityService.AddSingle(dictionary, "urcorner;", HtmlEntityService.Convert(8989));
			HtmlEntityService.AddSingle(dictionary, "urcrop;", HtmlEntityService.Convert(8974));
			HtmlEntityService.AddSingle(dictionary, "uring;", HtmlEntityService.Convert(367));
			HtmlEntityService.AddSingle(dictionary, "urtri;", HtmlEntityService.Convert(9721));
			HtmlEntityService.AddSingle(dictionary, "uscr;", HtmlEntityService.Convert(120010));
			HtmlEntityService.AddSingle(dictionary, "utdot;", HtmlEntityService.Convert(8944));
			HtmlEntityService.AddSingle(dictionary, "utilde;", HtmlEntityService.Convert(361));
			HtmlEntityService.AddSingle(dictionary, "utri;", HtmlEntityService.Convert(9653));
			HtmlEntityService.AddSingle(dictionary, "utrif;", HtmlEntityService.Convert(9652));
			HtmlEntityService.AddSingle(dictionary, "uuarr;", HtmlEntityService.Convert(8648));
			HtmlEntityService.AddBoth(dictionary, "uuml;", HtmlEntityService.Convert(252));
			HtmlEntityService.AddSingle(dictionary, "uwangle;", HtmlEntityService.Convert(10663));
			return dictionary;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002B03C File Offset: 0x0002923C
		private Dictionary<string, string> GetSymbolBigU()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "Uacute;", HtmlEntityService.Convert(218));
			HtmlEntityService.AddSingle(dictionary, "Uarr;", HtmlEntityService.Convert(8607));
			HtmlEntityService.AddSingle(dictionary, "Uarrocir;", HtmlEntityService.Convert(10569));
			HtmlEntityService.AddSingle(dictionary, "Ubrcy;", HtmlEntityService.Convert(1038));
			HtmlEntityService.AddSingle(dictionary, "Ubreve;", HtmlEntityService.Convert(364));
			HtmlEntityService.AddBoth(dictionary, "Ucirc;", HtmlEntityService.Convert(219));
			HtmlEntityService.AddSingle(dictionary, "Ucy;", HtmlEntityService.Convert(1059));
			HtmlEntityService.AddSingle(dictionary, "Udblac;", HtmlEntityService.Convert(368));
			HtmlEntityService.AddSingle(dictionary, "Ufr;", HtmlEntityService.Convert(120088));
			HtmlEntityService.AddBoth(dictionary, "Ugrave;", HtmlEntityService.Convert(217));
			HtmlEntityService.AddSingle(dictionary, "Umacr;", HtmlEntityService.Convert(362));
			HtmlEntityService.AddSingle(dictionary, "UnderBar;", HtmlEntityService.Convert(95));
			HtmlEntityService.AddSingle(dictionary, "UnderBrace;", HtmlEntityService.Convert(9183));
			HtmlEntityService.AddSingle(dictionary, "UnderBracket;", HtmlEntityService.Convert(9141));
			HtmlEntityService.AddSingle(dictionary, "UnderParenthesis;", HtmlEntityService.Convert(9181));
			HtmlEntityService.AddSingle(dictionary, "Union;", HtmlEntityService.Convert(8899));
			HtmlEntityService.AddSingle(dictionary, "UnionPlus;", HtmlEntityService.Convert(8846));
			HtmlEntityService.AddSingle(dictionary, "Uogon;", HtmlEntityService.Convert(370));
			HtmlEntityService.AddSingle(dictionary, "Uopf;", HtmlEntityService.Convert(120140));
			HtmlEntityService.AddSingle(dictionary, "UpArrow;", HtmlEntityService.Convert(8593));
			HtmlEntityService.AddSingle(dictionary, "Uparrow;", HtmlEntityService.Convert(8657));
			HtmlEntityService.AddSingle(dictionary, "UpArrowBar;", HtmlEntityService.Convert(10514));
			HtmlEntityService.AddSingle(dictionary, "UpArrowDownArrow;", HtmlEntityService.Convert(8645));
			HtmlEntityService.AddSingle(dictionary, "UpDownArrow;", HtmlEntityService.Convert(8597));
			HtmlEntityService.AddSingle(dictionary, "Updownarrow;", HtmlEntityService.Convert(8661));
			HtmlEntityService.AddSingle(dictionary, "UpEquilibrium;", HtmlEntityService.Convert(10606));
			HtmlEntityService.AddSingle(dictionary, "UpperLeftArrow;", HtmlEntityService.Convert(8598));
			HtmlEntityService.AddSingle(dictionary, "UpperRightArrow;", HtmlEntityService.Convert(8599));
			HtmlEntityService.AddSingle(dictionary, "Upsi;", HtmlEntityService.Convert(978));
			HtmlEntityService.AddSingle(dictionary, "Upsilon;", HtmlEntityService.Convert(933));
			HtmlEntityService.AddSingle(dictionary, "UpTee;", HtmlEntityService.Convert(8869));
			HtmlEntityService.AddSingle(dictionary, "UpTeeArrow;", HtmlEntityService.Convert(8613));
			HtmlEntityService.AddSingle(dictionary, "Uring;", HtmlEntityService.Convert(366));
			HtmlEntityService.AddSingle(dictionary, "Uscr;", HtmlEntityService.Convert(119984));
			HtmlEntityService.AddSingle(dictionary, "Utilde;", HtmlEntityService.Convert(360));
			HtmlEntityService.AddBoth(dictionary, "Uuml;", HtmlEntityService.Convert(220));
			return dictionary;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002B340 File Offset: 0x00029540
		private Dictionary<string, string> GetSymbolLittleV()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "vangrt;", HtmlEntityService.Convert(10652));
			HtmlEntityService.AddSingle(dictionary, "varepsilon;", HtmlEntityService.Convert(1013));
			HtmlEntityService.AddSingle(dictionary, "varkappa;", HtmlEntityService.Convert(1008));
			HtmlEntityService.AddSingle(dictionary, "varnothing;", HtmlEntityService.Convert(8709));
			HtmlEntityService.AddSingle(dictionary, "varphi;", HtmlEntityService.Convert(981));
			HtmlEntityService.AddSingle(dictionary, "varpi;", HtmlEntityService.Convert(982));
			HtmlEntityService.AddSingle(dictionary, "varpropto;", HtmlEntityService.Convert(8733));
			HtmlEntityService.AddSingle(dictionary, "vArr;", HtmlEntityService.Convert(8661));
			HtmlEntityService.AddSingle(dictionary, "varr;", HtmlEntityService.Convert(8597));
			HtmlEntityService.AddSingle(dictionary, "varrho;", HtmlEntityService.Convert(1009));
			HtmlEntityService.AddSingle(dictionary, "varsigma;", HtmlEntityService.Convert(962));
			HtmlEntityService.AddSingle(dictionary, "varsubsetneq;", HtmlEntityService.Convert(8842, 65024));
			HtmlEntityService.AddSingle(dictionary, "varsubsetneqq;", HtmlEntityService.Convert(10955, 65024));
			HtmlEntityService.AddSingle(dictionary, "varsupsetneq;", HtmlEntityService.Convert(8843, 65024));
			HtmlEntityService.AddSingle(dictionary, "varsupsetneqq;", HtmlEntityService.Convert(10956, 65024));
			HtmlEntityService.AddSingle(dictionary, "vartheta;", HtmlEntityService.Convert(977));
			HtmlEntityService.AddSingle(dictionary, "vartriangleleft;", HtmlEntityService.Convert(8882));
			HtmlEntityService.AddSingle(dictionary, "vartriangleright;", HtmlEntityService.Convert(8883));
			HtmlEntityService.AddSingle(dictionary, "vBar;", HtmlEntityService.Convert(10984));
			HtmlEntityService.AddSingle(dictionary, "vBarv;", HtmlEntityService.Convert(10985));
			HtmlEntityService.AddSingle(dictionary, "vcy;", HtmlEntityService.Convert(1074));
			HtmlEntityService.AddSingle(dictionary, "vDash;", HtmlEntityService.Convert(8872));
			HtmlEntityService.AddSingle(dictionary, "vdash;", HtmlEntityService.Convert(8866));
			HtmlEntityService.AddSingle(dictionary, "vee;", HtmlEntityService.Convert(8744));
			HtmlEntityService.AddSingle(dictionary, "veebar;", HtmlEntityService.Convert(8891));
			HtmlEntityService.AddSingle(dictionary, "veeeq;", HtmlEntityService.Convert(8794));
			HtmlEntityService.AddSingle(dictionary, "vellip;", HtmlEntityService.Convert(8942));
			HtmlEntityService.AddSingle(dictionary, "verbar;", HtmlEntityService.Convert(124));
			HtmlEntityService.AddSingle(dictionary, "vert;", HtmlEntityService.Convert(124));
			HtmlEntityService.AddSingle(dictionary, "vfr;", HtmlEntityService.Convert(120115));
			HtmlEntityService.AddSingle(dictionary, "vltri;", HtmlEntityService.Convert(8882));
			HtmlEntityService.AddSingle(dictionary, "vnsub;", HtmlEntityService.Convert(8834, 8402));
			HtmlEntityService.AddSingle(dictionary, "vnsup;", HtmlEntityService.Convert(8835, 8402));
			HtmlEntityService.AddSingle(dictionary, "vopf;", HtmlEntityService.Convert(120167));
			HtmlEntityService.AddSingle(dictionary, "vprop;", HtmlEntityService.Convert(8733));
			HtmlEntityService.AddSingle(dictionary, "vrtri;", HtmlEntityService.Convert(8883));
			HtmlEntityService.AddSingle(dictionary, "vscr;", HtmlEntityService.Convert(120011));
			HtmlEntityService.AddSingle(dictionary, "vsubnE;", HtmlEntityService.Convert(10955, 65024));
			HtmlEntityService.AddSingle(dictionary, "vsubne;", HtmlEntityService.Convert(8842, 65024));
			HtmlEntityService.AddSingle(dictionary, "vsupnE;", HtmlEntityService.Convert(10956, 65024));
			HtmlEntityService.AddSingle(dictionary, "vsupne;", HtmlEntityService.Convert(8843, 65024));
			HtmlEntityService.AddSingle(dictionary, "vzigzag;", HtmlEntityService.Convert(10650));
			return dictionary;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002B6F0 File Offset: 0x000298F0
		private Dictionary<string, string> GetSymbolBigV()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Vbar;", HtmlEntityService.Convert(10987));
			HtmlEntityService.AddSingle(dictionary, "Vcy;", HtmlEntityService.Convert(1042));
			HtmlEntityService.AddSingle(dictionary, "VDash;", HtmlEntityService.Convert(8875));
			HtmlEntityService.AddSingle(dictionary, "Vdash;", HtmlEntityService.Convert(8873));
			HtmlEntityService.AddSingle(dictionary, "Vdashl;", HtmlEntityService.Convert(10982));
			HtmlEntityService.AddSingle(dictionary, "Vee;", HtmlEntityService.Convert(8897));
			HtmlEntityService.AddSingle(dictionary, "Verbar;", HtmlEntityService.Convert(8214));
			HtmlEntityService.AddSingle(dictionary, "Vert;", HtmlEntityService.Convert(8214));
			HtmlEntityService.AddSingle(dictionary, "VerticalBar;", HtmlEntityService.Convert(8739));
			HtmlEntityService.AddSingle(dictionary, "VerticalLine;", HtmlEntityService.Convert(124));
			HtmlEntityService.AddSingle(dictionary, "VerticalSeparator;", HtmlEntityService.Convert(10072));
			HtmlEntityService.AddSingle(dictionary, "VerticalTilde;", HtmlEntityService.Convert(8768));
			HtmlEntityService.AddSingle(dictionary, "VeryThinSpace;", HtmlEntityService.Convert(8202));
			HtmlEntityService.AddSingle(dictionary, "Vfr;", HtmlEntityService.Convert(120089));
			HtmlEntityService.AddSingle(dictionary, "Vopf;", HtmlEntityService.Convert(120141));
			HtmlEntityService.AddSingle(dictionary, "Vscr;", HtmlEntityService.Convert(119985));
			HtmlEntityService.AddSingle(dictionary, "Vvdash;", HtmlEntityService.Convert(8874));
			return dictionary;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002B864 File Offset: 0x00029A64
		private Dictionary<string, string> GetSymbolLittleW()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "wcirc;", HtmlEntityService.Convert(373));
			HtmlEntityService.AddSingle(dictionary, "wedbar;", HtmlEntityService.Convert(10847));
			HtmlEntityService.AddSingle(dictionary, "wedge;", HtmlEntityService.Convert(8743));
			HtmlEntityService.AddSingle(dictionary, "wedgeq;", HtmlEntityService.Convert(8793));
			HtmlEntityService.AddSingle(dictionary, "weierp;", HtmlEntityService.Convert(8472));
			HtmlEntityService.AddSingle(dictionary, "wfr;", HtmlEntityService.Convert(120116));
			HtmlEntityService.AddSingle(dictionary, "wopf;", HtmlEntityService.Convert(120168));
			HtmlEntityService.AddSingle(dictionary, "wp;", HtmlEntityService.Convert(8472));
			HtmlEntityService.AddSingle(dictionary, "wr;", HtmlEntityService.Convert(8768));
			HtmlEntityService.AddSingle(dictionary, "wreath;", HtmlEntityService.Convert(8768));
			HtmlEntityService.AddSingle(dictionary, "wscr;", HtmlEntityService.Convert(120012));
			return dictionary;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0002B960 File Offset: 0x00029B60
		private Dictionary<string, string> GetSymbolBigW()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Wcirc;", HtmlEntityService.Convert(372));
			HtmlEntityService.AddSingle(dictionary, "Wedge;", HtmlEntityService.Convert(8896));
			HtmlEntityService.AddSingle(dictionary, "Wfr;", HtmlEntityService.Convert(120090));
			HtmlEntityService.AddSingle(dictionary, "Wopf;", HtmlEntityService.Convert(120142));
			HtmlEntityService.AddSingle(dictionary, "Wscr;", HtmlEntityService.Convert(119986));
			return dictionary;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0002B9DC File Offset: 0x00029BDC
		private Dictionary<string, string> GetSymbolLittleX()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "xcap;", HtmlEntityService.Convert(8898));
			HtmlEntityService.AddSingle(dictionary, "xcirc;", HtmlEntityService.Convert(9711));
			HtmlEntityService.AddSingle(dictionary, "xcup;", HtmlEntityService.Convert(8899));
			HtmlEntityService.AddSingle(dictionary, "xdtri;", HtmlEntityService.Convert(9661));
			HtmlEntityService.AddSingle(dictionary, "xfr;", HtmlEntityService.Convert(120117));
			HtmlEntityService.AddSingle(dictionary, "xhArr;", HtmlEntityService.Convert(10234));
			HtmlEntityService.AddSingle(dictionary, "xharr;", HtmlEntityService.Convert(10231));
			HtmlEntityService.AddSingle(dictionary, "xi;", HtmlEntityService.Convert(958));
			HtmlEntityService.AddSingle(dictionary, "xlArr;", HtmlEntityService.Convert(10232));
			HtmlEntityService.AddSingle(dictionary, "xlarr;", HtmlEntityService.Convert(10229));
			HtmlEntityService.AddSingle(dictionary, "xmap;", HtmlEntityService.Convert(10236));
			HtmlEntityService.AddSingle(dictionary, "xnis;", HtmlEntityService.Convert(8955));
			HtmlEntityService.AddSingle(dictionary, "xodot;", HtmlEntityService.Convert(10752));
			HtmlEntityService.AddSingle(dictionary, "xopf;", HtmlEntityService.Convert(120169));
			HtmlEntityService.AddSingle(dictionary, "xoplus;", HtmlEntityService.Convert(10753));
			HtmlEntityService.AddSingle(dictionary, "xotime;", HtmlEntityService.Convert(10754));
			HtmlEntityService.AddSingle(dictionary, "xrArr;", HtmlEntityService.Convert(10233));
			HtmlEntityService.AddSingle(dictionary, "xrarr;", HtmlEntityService.Convert(10230));
			HtmlEntityService.AddSingle(dictionary, "xscr;", HtmlEntityService.Convert(120013));
			HtmlEntityService.AddSingle(dictionary, "xsqcup;", HtmlEntityService.Convert(10758));
			HtmlEntityService.AddSingle(dictionary, "xuplus;", HtmlEntityService.Convert(10756));
			HtmlEntityService.AddSingle(dictionary, "xutri;", HtmlEntityService.Convert(9651));
			HtmlEntityService.AddSingle(dictionary, "xvee;", HtmlEntityService.Convert(8897));
			HtmlEntityService.AddSingle(dictionary, "xwedge;", HtmlEntityService.Convert(8896));
			return dictionary;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002BBE8 File Offset: 0x00029DE8
		private Dictionary<string, string> GetSymbolBigX()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Xfr;", HtmlEntityService.Convert(120091));
			HtmlEntityService.AddSingle(dictionary, "Xi;", HtmlEntityService.Convert(926));
			HtmlEntityService.AddSingle(dictionary, "Xopf;", HtmlEntityService.Convert(120143));
			HtmlEntityService.AddSingle(dictionary, "Xscr;", HtmlEntityService.Convert(119987));
			return dictionary;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002BC50 File Offset: 0x00029E50
		private Dictionary<string, string> GetSymbolLittleY()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "yacute;", HtmlEntityService.Convert(253));
			HtmlEntityService.AddSingle(dictionary, "yacy;", HtmlEntityService.Convert(1103));
			HtmlEntityService.AddSingle(dictionary, "ycirc;", HtmlEntityService.Convert(375));
			HtmlEntityService.AddSingle(dictionary, "ycy;", HtmlEntityService.Convert(1099));
			HtmlEntityService.AddBoth(dictionary, "yen;", HtmlEntityService.Convert(165));
			HtmlEntityService.AddSingle(dictionary, "yfr;", HtmlEntityService.Convert(120118));
			HtmlEntityService.AddSingle(dictionary, "yicy;", HtmlEntityService.Convert(1111));
			HtmlEntityService.AddSingle(dictionary, "yopf;", HtmlEntityService.Convert(120170));
			HtmlEntityService.AddSingle(dictionary, "yscr;", HtmlEntityService.Convert(120014));
			HtmlEntityService.AddSingle(dictionary, "yucy;", HtmlEntityService.Convert(1102));
			HtmlEntityService.AddBoth(dictionary, "yuml;", HtmlEntityService.Convert(255));
			return dictionary;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002BD4C File Offset: 0x00029F4C
		private Dictionary<string, string> GetSymbolBigY()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddBoth(dictionary, "Yacute;", HtmlEntityService.Convert(221));
			HtmlEntityService.AddSingle(dictionary, "YAcy;", HtmlEntityService.Convert(1071));
			HtmlEntityService.AddSingle(dictionary, "Ycirc;", HtmlEntityService.Convert(374));
			HtmlEntityService.AddSingle(dictionary, "Ycy;", HtmlEntityService.Convert(1067));
			HtmlEntityService.AddSingle(dictionary, "Yfr;", HtmlEntityService.Convert(120092));
			HtmlEntityService.AddSingle(dictionary, "YIcy;", HtmlEntityService.Convert(1031));
			HtmlEntityService.AddSingle(dictionary, "Yopf;", HtmlEntityService.Convert(120144));
			HtmlEntityService.AddSingle(dictionary, "Yscr;", HtmlEntityService.Convert(119988));
			HtmlEntityService.AddSingle(dictionary, "YUcy;", HtmlEntityService.Convert(1070));
			HtmlEntityService.AddSingle(dictionary, "Yuml;", HtmlEntityService.Convert(376));
			return dictionary;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002BE30 File Offset: 0x0002A030
		private Dictionary<string, string> GetSymbolLittleZ()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "zacute;", HtmlEntityService.Convert(378));
			HtmlEntityService.AddSingle(dictionary, "zcaron;", HtmlEntityService.Convert(382));
			HtmlEntityService.AddSingle(dictionary, "zcy;", HtmlEntityService.Convert(1079));
			HtmlEntityService.AddSingle(dictionary, "zdot;", HtmlEntityService.Convert(380));
			HtmlEntityService.AddSingle(dictionary, "zeetrf;", HtmlEntityService.Convert(8488));
			HtmlEntityService.AddSingle(dictionary, "zeta;", HtmlEntityService.Convert(950));
			HtmlEntityService.AddSingle(dictionary, "zfr;", HtmlEntityService.Convert(120119));
			HtmlEntityService.AddSingle(dictionary, "zhcy;", HtmlEntityService.Convert(1078));
			HtmlEntityService.AddSingle(dictionary, "zigrarr;", HtmlEntityService.Convert(8669));
			HtmlEntityService.AddSingle(dictionary, "zopf;", HtmlEntityService.Convert(120171));
			HtmlEntityService.AddSingle(dictionary, "zscr;", HtmlEntityService.Convert(120015));
			HtmlEntityService.AddSingle(dictionary, "zwj;", HtmlEntityService.Convert(8205));
			HtmlEntityService.AddSingle(dictionary, "zwnj;", HtmlEntityService.Convert(8204));
			return dictionary;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0002BF54 File Offset: 0x0002A154
		private Dictionary<string, string> GetSymbolBigZ()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			HtmlEntityService.AddSingle(dictionary, "Zacute;", HtmlEntityService.Convert(377));
			HtmlEntityService.AddSingle(dictionary, "Zcaron;", HtmlEntityService.Convert(381));
			HtmlEntityService.AddSingle(dictionary, "Zcy;", HtmlEntityService.Convert(1047));
			HtmlEntityService.AddSingle(dictionary, "Zdot;", HtmlEntityService.Convert(379));
			HtmlEntityService.AddSingle(dictionary, "ZeroWidthSpace;", HtmlEntityService.Convert(8203));
			HtmlEntityService.AddSingle(dictionary, "Zeta;", HtmlEntityService.Convert(918));
			HtmlEntityService.AddSingle(dictionary, "Zfr;", HtmlEntityService.Convert(8488));
			HtmlEntityService.AddSingle(dictionary, "ZHcy;", HtmlEntityService.Convert(1046));
			HtmlEntityService.AddSingle(dictionary, "Zopf;", HtmlEntityService.Convert(8484));
			HtmlEntityService.AddSingle(dictionary, "Zscr;", HtmlEntityService.Convert(119989));
			return dictionary;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0002C038 File Offset: 0x0002A238
		public string GetSymbol(string name)
		{
			string text = null;
			Dictionary<string, string> dictionary = null;
			if (!string.IsNullOrEmpty(name) && this._entities.TryGetValue(name[0], out dictionary))
			{
				dictionary.TryGetValue(name, out text);
			}
			return text;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0002C072 File Offset: 0x0002A272
		private static string Convert(int code)
		{
			return code.ConvertFromUtf32();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0002C07A File Offset: 0x0002A27A
		private static string Convert(int leading, int trailing)
		{
			return leading.ConvertFromUtf32() + trailing.ConvertFromUtf32();
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0002C08D File Offset: 0x0002A28D
		public static bool IsInvalidNumber(int code)
		{
			return (code >= 55296 && code <= 57343) || code > 1114111;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0002C0AC File Offset: 0x0002A2AC
		public static bool IsInCharacterTable(int code)
		{
			return code == 0 || code == 13 || code == 128 || code == 129 || code == 130 || code == 131 || code == 132 || code == 133 || code == 134 || code == 135 || code == 136 || code == 137 || code == 138 || code == 139 || code == 140 || code == 141 || code == 142 || code == 143 || code == 144 || code == 145 || code == 146 || code == 147 || code == 148 || code == 149 || code == 150 || code == 151 || code == 152 || code == 153 || code == 154 || code == 155 || code == 156 || code == 157 || code == 158 || code == 159;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0002C1FC File Offset: 0x0002A3FC
		public static string GetSymbolFromTable(int code)
		{
			if (code == 0)
			{
				return HtmlEntityService.Convert(65533);
			}
			if (code == 13)
			{
				return HtmlEntityService.Convert(13);
			}
			switch (code)
			{
			case 128:
				return HtmlEntityService.Convert(8364);
			case 129:
				return HtmlEntityService.Convert(129);
			case 130:
				return HtmlEntityService.Convert(8218);
			case 131:
				return HtmlEntityService.Convert(402);
			case 132:
				return HtmlEntityService.Convert(8222);
			case 133:
				return HtmlEntityService.Convert(8230);
			case 134:
				return HtmlEntityService.Convert(8224);
			case 135:
				return HtmlEntityService.Convert(8225);
			case 136:
				return HtmlEntityService.Convert(710);
			case 137:
				return HtmlEntityService.Convert(8240);
			case 138:
				return HtmlEntityService.Convert(352);
			case 139:
				return HtmlEntityService.Convert(8249);
			case 140:
				return HtmlEntityService.Convert(338);
			case 141:
				return HtmlEntityService.Convert(141);
			case 142:
				return HtmlEntityService.Convert(381);
			case 143:
				return HtmlEntityService.Convert(143);
			case 144:
				return HtmlEntityService.Convert(144);
			case 145:
				return HtmlEntityService.Convert(8216);
			case 146:
				return HtmlEntityService.Convert(8217);
			case 147:
				return HtmlEntityService.Convert(8220);
			case 148:
				return HtmlEntityService.Convert(8221);
			case 149:
				return HtmlEntityService.Convert(8226);
			case 150:
				return HtmlEntityService.Convert(8211);
			case 151:
				return HtmlEntityService.Convert(8212);
			case 152:
				return HtmlEntityService.Convert(732);
			case 153:
				return HtmlEntityService.Convert(8482);
			case 154:
				return HtmlEntityService.Convert(353);
			case 155:
				return HtmlEntityService.Convert(8250);
			case 156:
				return HtmlEntityService.Convert(339);
			case 157:
				return HtmlEntityService.Convert(157);
			case 158:
				return HtmlEntityService.Convert(382);
			case 159:
				return HtmlEntityService.Convert(376);
			default:
				return null;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002C41C File Offset: 0x0002A61C
		public static bool IsInInvalidRange(int code)
		{
			return (code >= 1 && code <= 8) || (code >= 14 && code <= 31) || (code >= 127 && code <= 159) || (code >= 64976 && code <= 65007) || code == 11 || code == 65534 || code == 65535 || code == 131070 || code == 196606 || code == 131071 || code == 196607 || code == 262142 || code == 262143 || code == 327678 || code == 327679 || code == 393214 || code == 393215 || code == 458750 || code == 458751 || code == 524286 || code == 524287 || code == 589822 || code == 589823 || code == 655358 || code == 655359 || code == 720894 || code == 720895 || code == 786430 || code == 786431 || code == 851966 || code == 851967 || code == 917502 || code == 917503 || code == 983038 || code == 983039 || code == 1048574 || code == 1048575 || code == 1114110 || code == 1114111;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002C5B4 File Offset: 0x0002A7B4
		private static void AddSingle(Dictionary<string, string> symbols, string key, string value)
		{
			symbols.Add(key, value);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0002C5BE File Offset: 0x0002A7BE
		private static void AddBoth(Dictionary<string, string> symbols, string key, string value)
		{
			symbols.Add(key, value);
			symbols.Add(key.Remove(key.Length - 1), value);
		}

		// Token: 0x040004E4 RID: 1252
		private readonly Dictionary<char, Dictionary<string, string>> _entities;

		// Token: 0x040004E5 RID: 1253
		public static readonly IEntityProvider Resolver = new HtmlEntityService();
	}
}
