﻿using Decal.Adapter.Wrappers;
using System;
using System.Collections.Generic;

namespace XpAllocator
{
    internal static class GameConstants
    {
        public static IList<long> VitalXpTable { get; private set; }
        public static IList<long> TrainedSkillXpTable { get; private set; }
        public static IList<long> SpecSkillXpTable { get; private set; }
        public static IList<long> AttributeXpTable { get; private set; }
        public static IList<SkillData> SkillData{ get; private set; }
        public static IList<string> NonSkillTraits { get; private set; }
        static GameConstants() {
            VitalXpTable = GetVitalXpTable().AsReadOnly();
            AttributeXpTable = GetAttributeXpTable().AsReadOnly();
            TrainedSkillXpTable = GetTrainSkillXpTable().AsReadOnly();
            SpecSkillXpTable = GetSpecSkillXpTable().AsReadOnly();
            SkillData = GetSkillData().AsReadOnly();
            NonSkillTraits = NonSkillTraitsData().AsReadOnly();
        }

        private static List<string> NonSkillTraitsData()
        {
            var rv = new List<string>
            {
                "strength",
                "endurance",
                "coordination",
                "quickness",
                "focus",
                "self",
                "health",
                "stamina",
                "mana"
            };

            return rv;
        }

        private static List<long> GetAttributeXpTable()
        {
            List<long> rv = new List<long>();
            rv.Add(0);
            rv.Add(110);
            rv.Add(277);
            rv.Add(501);
            rv.Add(784);
            rv.Add(1125);
            rv.Add(1527);
            rv.Add(1988);
            rv.Add(2511);
            rv.Add(3097);
            rv.Add(3746);
            rv.Add(4459);
            rv.Add(5238);
            rv.Add(6084);
            rv.Add(6998);
            rv.Add(7982);
            rv.Add(9038);
            rv.Add(10167);
            rv.Add(11372);
            rv.Add(12654);
            rv.Add(14015);
            rv.Add(15459);
            rv.Add(16988);
            rv.Add(18604);
            rv.Add(20311);
            rv.Add(22113);
            rv.Add(24012);
            rv.Add(26014);
            rv.Add(28122);
            rv.Add(30341);
            rv.Add(32676);
            rv.Add(35132);
            rv.Add(37716);
            rv.Add(40434);
            rv.Add(43293);
            rv.Add(46301);
            rv.Add(49465);
            rv.Add(52795);
            rv.Add(56300);
            rv.Add(59991);
            rv.Add(63878);
            rv.Add(67975);
            rv.Add(72295);
            rv.Add(76851);
            rv.Add(81659);
            rv.Add(86737);
            rv.Add(92102);
            rv.Add(97775);
            rv.Add(103775);
            rv.Add(110128);
            rv.Add(116858);
            rv.Add(123991);
            rv.Add(131559);
            rv.Add(139591);
            rv.Add(148124);
            rv.Add(157194);
            rv.Add(166843);
            rv.Add(177113);
            rv.Add(188053);
            rv.Add(199715);
            rv.Add(212153);
            rv.Add(225429);
            rv.Add(239609);
            rv.Add(254762);
            rv.Add(270967);
            rv.Add(288306);
            rv.Add(306870);
            rv.Add(326756);
            rv.Add(348070);
            rv.Add(370928);
            rv.Add(395453);
            rv.Add(421779);
            rv.Add(450054);
            rv.Add(480434);
            rv.Add(513091);
            rv.Add(548210);
            rv.Add(585992);
            rv.Add(626654);
            rv.Add(670432);
            rv.Add(717582);
            rv.Add(768378);
            rv.Add(823122);
            rv.Add(882136);
            rv.Add(945773);
            rv.Add(1014414);
            rv.Add(1088469);
            rv.Add(1168386);
            rv.Add(1254649);
            rv.Add(1347781);
            rv.Add(1448351);
            rv.Add(1556972);
            rv.Add(1674311);
            rv.Add(1801089);
            rv.Add(1938088);
            rv.Add(2086155);
            rv.Add(2246205);
            rv.Add(2419233);
            rv.Add(2606314);
            rv.Add(2808613);
            rv.Add(3027394);
            rv.Add(3264023);
            rv.Add(3519983);
            rv.Add(3796877);
            rv.Add(4096444);
            rv.Add(4420567);
            rv.Add(4771285);
            rv.Add(5150808);
            rv.Add(5561528);
            rv.Add(6006039);
            rv.Add(6487148);
            rv.Add(7007896);
            rv.Add(7571580);
            rv.Add(8181768);
            rv.Add(8842327);
            rv.Add(9557443);
            rv.Add(10331656);
            rv.Add(11169877);
            rv.Add(12077431);
            rv.Add(13060084);
            rv.Add(14124082);
            rv.Add(15276190);
            rv.Add(16523738);
            rv.Add(17874666);
            rv.Add(19337572);
            rv.Add(20921773);
            rv.Add(22637359);
            rv.Add(24495261);
            rv.Add(26507320);
            rv.Add(28686361);
            rv.Add(31046278);
            rv.Add(33602120);
            rv.Add(36370190);
            rv.Add(39368147);
            rv.Add(42615120);
            rv.Add(46131828);
            rv.Add(49940719);
            rv.Add(54066105);
            rv.Add(58534323);
            rv.Add(63373901);
            rv.Add(68615745);
            rv.Add(74293328);
            rv.Add(80442912);
            rv.Add(87103777);
            rv.Add(94318471);
            rv.Add(102133083);
            rv.Add(110597540);
            rv.Add(119765922);
            rv.Add(129696811);
            rv.Add(140453665);
            rv.Add(152105222);
            rv.Add(164725942);
            rv.Add(178396483);
            rv.Add(193204214);
            rv.Add(209243776);
            rv.Add(226617688);
            rv.Add(245437001);
            rv.Add(265822007);
            rv.Add(287903011);
            rv.Add(311821164);
            rv.Add(337729361);
            rv.Add(365793227);
            rv.Add(396192167);
            rv.Add(429120520);
            rv.Add(464788799);
            rv.Add(503425038);
            rv.Add(545276249);
            rv.Add(590610001);
            rv.Add(639716134);
            rv.Add(692908610);
            rv.Add(750527522);
            rv.Add(812941268);
            rv.Add(880548904);
            rv.Add(953782704);
            rv.Add(1033110914);
            rv.Add(1119040753);
            rv.Add(1212121655);
            rv.Add(1312948783);
            rv.Add(1422166831);
            rv.Add(1540474151);
            rv.Add(1668627219);
            rv.Add(1807445467);
            rv.Add(1957816530);
            rv.Add(2120701915);
            rv.Add(2297143157);
            rv.Add(2488268472);
            rv.Add(2695299977);
            rv.Add(2919561502);
            rv.Add(3162487055);
            rv.Add(3425629996);
            rv.Add(3710672964);
            rv.Add(4019438644);

            return rv;
        }
        private static List<long> GetSpecSkillXpTable()
        {
            List<long> rv = new List<long>();

            rv.Add(0);
            rv.Add(23);
            rv.Add(56);
            rv.Add(97);
            rv.Add(149);
            rv.Add(211);
            rv.Add(282);
            rv.Add(364);
            rv.Add(456);
            rv.Add(558);
            rv.Add(671);
            rv.Add(795);
            rv.Add(931);
            rv.Add(1077);
            rv.Add(1236);
            rv.Add(1406);
            rv.Add(1589);
            rv.Add(1784);
            rv.Add(1992);
            rv.Add(2214);
            rv.Add(2449);
            rv.Add(2699);
            rv.Add(2963);
            rv.Add(3243);
            rv.Add(3539);
            rv.Add(3850);
            rv.Add(4180);
            rv.Add(4527);
            rv.Add(4892);
            rv.Add(5277);
            rv.Add(5683);
            rv.Add(6109);
            rv.Add(6559);
            rv.Add(7031);
            rv.Add(7529);
            rv.Add(8052);
            rv.Add(8603);
            rv.Add(9183);
            rv.Add(9794);
            rv.Add(10437);
            rv.Add(11115);
            rv.Add(11829);
            rv.Add(12582);
            rv.Add(13376);
            rv.Add(14213);
            rv.Add(15098);
            rv.Add(16031);
            rv.Add(17018);
            rv.Add(18061);
            rv.Add(19165);
            rv.Add(20332);
            rv.Add(21569);
            rv.Add(22879);
            rv.Add(24267);
            rv.Add(25740);
            rv.Add(27304);
            rv.Add(28964);
            rv.Add(30728);
            rv.Add(32603);
            rv.Add(34597);
            rv.Add(36720);
            rv.Add(38981);
            rv.Add(41389);
            rv.Add(43956);
            rv.Add(46695);
            rv.Add(49616);
            rv.Add(52736);
            rv.Add(56067);
            rv.Add(59627);
            rv.Add(63433);
            rv.Add(67504);
            rv.Add(71859);
            rv.Add(76521);
            rv.Add(81513);
            rv.Add(86860);
            rv.Add(92590);
            rv.Add(98732);
            rv.Add(105319);
            rv.Add(112384);
            rv.Add(119965);
            rv.Add(128101);
            rv.Add(136836);
            rv.Add(146216);
            rv.Add(156291);
            rv.Add(167116);
            rv.Add(178749);
            rv.Add(191252);
            rv.Add(204694);
            rv.Add(219149);
            rv.Add(234694);
            rv.Add(251416);
            rv.Add(269407);
            rv.Add(288765);
            rv.Add(309599);
            rv.Add(332022);
            rv.Add(356161);
            rv.Add(382148);
            rv.Add(410131);
            rv.Add(440264);
            rv.Add(472717);
            rv.Add(507671);
            rv.Add(545324);
            rv.Add(585886);
            rv.Add(629586);
            rv.Add(676672);
            rv.Add(726408);
            rv.Add(777982);
            rv.Add(831204);
            rv.Add(886706);
            rv.Add(944149);
            rv.Add(1004623);
            rv.Add(1068144);
            rv.Add(1134867);
            rv.Add(1204278);
            rv.Add(1276904);
            rv.Add(1353312);
            rv.Add(1434114);
            rv.Add(1518971);
            rv.Add(1607595);
            rv.Add(1700755);
            rv.Add(1799280);
            rv.Add(1903065);
            rv.Add(2011073);
            rv.Add(2124346);
            rv.Add(2244006);
            rv.Add(2368266);
            rv.Add(2497430);
            rv.Add(2631909);
            rv.Add(2771224);
            rv.Add(2917013);
            rv.Add(3067048);
            rv.Add(3222235);
            rv.Add(3383635);
            rv.Add(3551467);
            rv.Add(3725130);
            rv.Add(3904206);
            rv.Add(4089485);
            rv.Add(4280974);
            rv.Add(4478917);
            rv.Add(4684816);
            rv.Add(4898446);
            rv.Add(5119881);
            rv.Add(5349513);
            rv.Add(5587084);
            rv.Add(5832707);
            rv.Add(6086897);
            rv.Add(6350606);
            rv.Add(6623252);
            rv.Add(6905759);
            rv.Add(7199598);
            rv.Add(7510827);
            rv.Add(7835138);
            rv.Add(8185908);
            rv.Add(8566254);
            rv.Add(8983087);
            rv.Add(9452180);
            rv.Add(9978231);
            rv.Add(10590938);
            rv.Add(11292080);
            rv.Add(12080597);
            rv.Add(12978687);
            rv.Add(13957900);
            rv.Add(14971249);
            rv.Add(16103320);
            rv.Add(17322402);
            rv.Add(18634617);
            rv.Add(20062065);
            rv.Add(21585981);
            rv.Add(23214900);
            rv.Add(24936844);
            rv.Add(26808511);
            rv.Add(28810492);
            rv.Add(30975492);
            rv.Add(33221583);
            rv.Add(35528463);
            rv.Add(38089744);
            rv.Add(40943261);
            rv.Add(43951402);
            rv.Add(47181470);
            rv.Add(50806066);
            rv.Add(54703511);
            rv.Add(59258291);
            rv.Add(64461548);
            rv.Add(70511600);
            rv.Add(77114508);
            rv.Add(84284685);
            rv.Add(92045555);
            rv.Add(100330262);
            rv.Add(109182433);
            rv.Add(118957009);
            rv.Add(129861131);
            rv.Add(141695103);
            rv.Add(154193427);
            rv.Add(167565923);
            rv.Add(183038936);
            rv.Add(200856634);
            rv.Add(221282414);
            rv.Add(244600416);
            rv.Add(271117157);
            rv.Add(301163291);
            rv.Add(336095513);
            rv.Add(374298608);
            rv.Add(418187661);
            rv.Add(466210448);
            rv.Add(520850007);
            rv.Add(581627417);
            rv.Add(648104789);
            rv.Add(721888505);
            rv.Add(802632699);
            rv.Add(890043017);
            rv.Add(984880677);
            rv.Add(1085966844);
            rv.Add(1196187351);
            rv.Add(1315497790);
            rv.Add(1443929007);
            rv.Add(1582593030);
            rv.Add(1730689458);
            rv.Add(1891512364);
            rv.Add(2064457725);
            rv.Add(2249031458);
            rv.Add(2449858070);
            rv.Add(2667631083);
            rv.Add(2902448781);
            rv.Add(3160874561);
            rv.Add(3440192563);
            rv.Add(3750444304);
            rv.Add(4100490438);

            return rv;
        }
        private static List<long> GetTrainSkillXpTable()
        {
            List<long> rv = new List<long>();

            rv.Add(0);
            rv.Add(58);
            rv.Add(138);
            rv.Add(243);
            rv.Add(372);
            rv.Add(526);
            rv.Add(704);
            rv.Add(908);
            rv.Add(1138);
            rv.Add(1395);
            rv.Add(1678);
            rv.Add(1988);
            rv.Add(2326);
            rv.Add(2693);
            rv.Add(3089);
            rv.Add(3515);
            rv.Add(3971);
            rv.Add(4459);
            rv.Add(4980);
            rv.Add(5534);
            rv.Add(6122);
            rv.Add(6747);
            rv.Add(7408);
            rv.Add(8107);
            rv.Add(8846);
            rv.Add(9625);
            rv.Add(10448);
            rv.Add(11316);
            rv.Add(12230);
            rv.Add(13192);
            rv.Add(14206);
            rv.Add(15273);
            rv.Add(16396);
            rv.Add(17578);
            rv.Add(18821);
            rv.Add(20130);
            rv.Add(21508);
            rv.Add(22958);
            rv.Add(24485);
            rv.Add(26092);
            rv.Add(27786);
            rv.Add(29572);
            rv.Add(31454);
            rv.Add(33438);
            rv.Add(35533);
            rv.Add(37743);
            rv.Add(40078);
            rv.Add(42545);
            rv.Add(45152);
            rv.Add(47911);
            rv.Add(50830);
            rv.Add(53921);
            rv.Add(57196);
            rv.Add(60668);
            rv.Add(64350);
            rv.Add(68259);
            rv.Add(72409);
            rv.Add(76818);
            rv.Add(81506);
            rv.Add(86493);
            rv.Add(91800);
            rv.Add(97451);
            rv.Add(103472);
            rv.Add(109890);
            rv.Add(116736);
            rv.Add(124040);
            rv.Add(131838);
            rv.Add(140167);
            rv.Add(149067);
            rv.Add(158582);
            rv.Add(168758);
            rv.Add(179646);
            rv.Add(191301);
            rv.Add(203781);
            rv.Add(217149);
            rv.Add(231474);
            rv.Add(246830);
            rv.Add(263297);
            rv.Add(280959);
            rv.Add(299911);
            rv.Add(320252);
            rv.Add(342089);
            rv.Add(365539);
            rv.Add(390727);
            rv.Add(417789);
            rv.Add(446871);
            rv.Add(478129);
            rv.Add(511735);
            rv.Add(547871);
            rv.Add(586735);
            rv.Add(628540);
            rv.Add(673517);
            rv.Add(721913);
            rv.Add(773996);
            rv.Add(830054);
            rv.Add(890401);
            rv.Add(955370);
            rv.Add(1025326);
            rv.Add(1100659);
            rv.Add(1181791);
            rv.Add(1269177);
            rv.Add(1363308);
            rv.Add(1464714);
            rv.Add(1573965);
            rv.Add(1691679);
            rv.Add(1818520);
            rv.Add(1955205);
            rv.Add(2102508);
            rv.Add(2261264);
            rv.Add(2432373);
            rv.Add(2616806);
            rv.Add(2815610);
            rv.Add(3029917);
            rv.Add(3260945);
            rv.Add(3510009);
            rv.Add(3778529);
            rv.Add(4068034);
            rv.Add(4380177);
            rv.Add(4716738);
            rv.Add(5079638);
            rv.Add(5470950);
            rv.Add(5892911);
            rv.Add(6347931);
            rv.Add(6838614);
            rv.Add(7367765);
            rv.Add(7938414);
            rv.Add(8553825);
            rv.Add(9217523);
            rv.Add(9933309);
            rv.Add(10705283);
            rv.Add(11537868);
            rv.Add(12435837);
            rv.Add(13404336);
            rv.Add(14448918);
            rv.Add(15575574);
            rv.Add(16790764);
            rv.Add(18101461);
            rv.Add(19515183);
            rv.Add(21040043);
            rv.Add(22684790);
            rv.Add(24458865);
            rv.Add(26372451);
            rv.Add(28436532);
            rv.Add(30662960);
            rv.Add(33064516);
            rv.Add(35654992);
            rv.Add(38449264);
            rv.Add(41463378);
            rv.Add(44714647);
            rv.Add(48221744);
            rv.Add(52004816);
            rv.Add(56085593);
            rv.Add(60487519);
            rv.Add(65235884);
            rv.Add(70357967);
            rv.Add(75883199);
            rv.Add(81843326);
            rv.Add(88272594);
            rv.Add(95207949);
            rv.Add(102689242);
            rv.Add(110759467);
            rv.Add(119465000);
            rv.Add(128855871);
            rv.Add(138986049);
            rv.Add(149913755);
            rv.Add(161701793);
            rv.Add(174417913);
            rv.Add(188135201);
            rv.Add(202932500);
            rv.Add(218894860);
            rv.Add(236114028);
            rv.Add(254688979);
            rv.Add(274726480);
            rv.Add(296341707);
            rv.Add(319658907);
            rv.Add(344812110);
            rv.Add(371945902);
            rv.Add(401216255);
            rv.Add(432791424);
            rv.Add(466852915);
            rv.Add(503596527);
            rv.Add(543233477);
            rv.Add(585991620);
            rv.Add(632116749);
            rv.Add(681874018);
            rv.Add(735549461);
            rv.Add(793451636);
            rv.Add(855913403);
            rv.Add(923293832);
            rv.Add(995980273);
            rv.Add(1074390578);
            rv.Add(1158975507);
            rv.Add(1250221316);
            rv.Add(1348652558);
            rv.Add(1454835090);
            rv.Add(1569379334);
            rv.Add(1692943784);
            rv.Add(1826238790);
            rv.Add(1970030642);
            rv.Add(2125145977);
            rv.Add(2292476532);
            rv.Add(2472984268);
            rv.Add(2667706901);
            rv.Add(2877763869);
            rv.Add(3104362767);
            rv.Add(3348806291);
            rv.Add(3612499722);
            rv.Add(3896959013);
            rv.Add(4203819496);

            return rv;
        }
        private static List<long> GetVitalXpTable()
        {
            List<long> rv = new List<long>();

            rv.Add(0);
            rv.Add(73);
            rv.Add(183);
            rv.Add(331);
            rv.Add(517);
            rv.Add(743);
            rv.Add(1008);
            rv.Add(1312);
            rv.Add(1658);
            rv.Add(2044);
            rv.Add(2472);
            rv.Add(2943);
            rv.Add(3457);
            rv.Add(4015);
            rv.Add(4619);
            rv.Add(5268);
            rv.Add(5965);
            rv.Add(6711);
            rv.Add(7505);
            rv.Add(8352);
            rv.Add(9250);
            rv.Add(10203);
            rv.Add(11212);
            rv.Add(12279);
            rv.Add(13406);
            rv.Add(14595);
            rv.Add(15848);
            rv.Add(17169);
            rv.Add(18561);
            rv.Add(20025);
            rv.Add(21566);
            rv.Add(23187);
            rv.Add(24893);
            rv.Add(26687);
            rv.Add(28574);
            rv.Add(30559);
            rv.Add(32647);
            rv.Add(34845);
            rv.Add(37158);
            rv.Add(39594);
            rv.Add(42160);
            rv.Add(44864);
            rv.Add(47715);
            rv.Add(50722);
            rv.Add(53895);
            rv.Add(57247);
            rv.Add(60788);
            rv.Add(64531);
            rv.Add(68492);
            rv.Add(72685);
            rv.Add(77126);
            rv.Add(81834);
            rv.Add(86829);
            rv.Add(92130);
            rv.Add(97762);
            rv.Add(103748);
            rv.Add(110116);
            rv.Add(116895);
            rv.Add(124115);
            rv.Add(131812);
            rv.Add(140021);
            rv.Add(148784);
            rv.Add(158142);
            rv.Add(168143);
            rv.Add(178838);
            rv.Add(190282);
            rv.Add(202534);
            rv.Add(215659);
            rv.Add(229726);
            rv.Add(244812);
            rv.Add(260999);
            rv.Add(278375);
            rv.Add(297036);
            rv.Add(317087);
            rv.Add(338640);
            rv.Add(361819);
            rv.Add(386755);
            rv.Add(413592);
            rv.Add(442486);
            rv.Add(473604);
            rv.Add(507130);
            rv.Add(543260);
            rv.Add(582210);
            rv.Add(624211);
            rv.Add(669513);
            rv.Add(718390);
            rv.Add(771135);
            rv.Add(828069);
            rv.Add(889536);
            rv.Add(955912);
            rv.Add(1027602);
            rv.Add(1105046);
            rv.Add(1188719);
            rv.Add(1279139);
            rv.Add(1376862);
            rv.Add(1482495);
            rv.Add(1596694);
            rv.Add(1720167);
            rv.Add(1853685);
            rv.Add(1998080);
            rv.Add(2154256);
            rv.Add(2323189);
            rv.Add(2505939);
            rv.Add(2703654);
            rv.Add(2917575);
            rv.Add(3149049);
            rv.Add(3399533);
            rv.Add(3670609);
            rv.Add(3963986);
            rv.Add(4281518);
            rv.Add(4625212);
            rv.Add(4997243);
            rv.Add(5399967);
            rv.Add(5835936);
            rv.Add(6307913);
            rv.Add(6818893);
            rv.Add(7372119);
            rv.Add(7971105);
            rv.Add(8619656);
            rv.Add(9321894);
            rv.Add(10082286);
            rv.Add(10905668);
            rv.Add(11797280);
            rv.Add(12762798);
            rv.Add(13808370);
            rv.Add(14940657);
            rv.Add(16166873);
            rv.Add(17494831);
            rv.Add(18932998);
            rv.Add(20490543);
            rv.Add(22177399);
            rv.Add(24004326);
            rv.Add(25982977);
            rv.Add(28125979);
            rv.Add(30447007);
            rv.Add(32960875);
            rv.Add(35683629);
            rv.Add(38632653);
            rv.Add(41826775);
            rv.Add(45286392);
            rv.Add(49033597);
            rv.Add(53092322);
            rv.Add(57488493);
            rv.Add(62250191);
            rv.Add(67407835);
            rv.Add(72994377);
            rv.Add(79045509);
            rv.Add(85599896);
            rv.Add(92699419);
            rv.Add(100389447);
            rv.Add(108719122);
            rv.Add(117741679);
            rv.Add(127514781);
            rv.Add(138100892);
            rv.Add(149567674);
            rv.Add(161988421);
            rv.Add(175442525);
            rv.Add(190015988);
            rv.Add(205801968);
            rv.Add(222901379);
            rv.Add(241423530);
            rv.Add(261486830);
            rv.Add(283219543);
            rv.Add(306760608);
            rv.Add(332260525);
            rv.Add(359882324);
            rv.Add(389802601);
            rv.Add(422212649);
            rv.Add(457319683);
            rv.Add(495348165);
            rv.Add(536541237);
            rv.Add(581162277);
            rv.Add(629496585);
            rv.Add(681853203);
            rv.Add(738566897);
            rv.Add(800000293);
            rv.Add(866546197);
            rv.Add(938630108);
            rv.Add(1016712940);
            rv.Add(1101293965);
            rv.Add(1192914009);
            rv.Add(1292158910);
            rv.Add(1399663264);
            rv.Add(1516114484);
            rv.Add(1642257192);
            rv.Add(1778897985);
            rv.Add(1926910591);
            rv.Add(2087241457);
            rv.Add(2260915797);
            rv.Add(2449044157);
            rv.Add(2652829505);
            rv.Add(2873574933);
            rv.Add(3112691986);
            rv.Add(3371709687);
            rv.Add(3652284316);
            rv.Add(3956210003);
            rv.Add(4285430197);

            return rv;
        }
        private static List<SkillData> GetSkillData()
        {
            var rv = new List<SkillData>() {
                new SkillData("meleedefense", 3, new List<string> { "coordination", "quickness" }),
                new SkillData("missiledefense", 5, new List<string> { "coordination", "quickness" }),
                new SkillData("arcanelore", 3, new List<string> { "focus" }),
                new SkillData("magicdefense", 7, new List<string> { "focus", "self" }),
                new SkillData("manaconversion", 6, new List<string> { "self", "focus" }),
                new SkillData("itemtinkering", 2, new List<string> { "focus", "self" }),
                new SkillData("healing", 3, new List<string> { "focus", "coordination" }),
                new SkillData("jump", 2, new List<string> { "coordination", "strength" }),
                new SkillData("lockpick", 3, new List<string> { "coordination", "strength" }),
                new SkillData("run", 1, new List<string> { "quickness" }),
                new SkillData("weapontinkering", 2, new List<string> { "strength", "focus" }),
                new SkillData("armortinkering", 2, new List<string> { "endurance", "focus" }),
                new SkillData("magicitemtinkering", 1, new List<string> { "focus" }),
                new SkillData("creatureenchantment", 4, new List<string> { "focus", "self" }),
                new SkillData("itemenchantment", 4, new List<string> { "focus", "self" }),
                new SkillData("lifemagic", 4, new List<string> { "focus", "self" }),
                new SkillData("warmagic", 4, new List<string> { "focus", "self" }),
                new SkillData("fletching", 3, new List<string> { "coordination", "focus" }),
                new SkillData("alchemy", 3, new List<string> { "coordination", "focus" }),
                new SkillData("cooking", 3, new List<string> { "coordination", "focus" }),
                new SkillData("twohandedcombat", 3, new List<string> { "strength", "coordination" }),
                new SkillData("voidmagic", 4, new List<string> { "focus", "self" }),
                new SkillData("heavyweapons", 3, new List<string> { "strength", "coordination" }),
                new SkillData("lightweapons", 3, new List<string> { "strength", "coordination" }),
                new SkillData("finesseweapons", 3, new List<string> { "coordination", "quickness" }),
                new SkillData("missileweapons", 2, new List<string> { "coordination" }),
                new SkillData("shield", 2, new List<string> { "strength", "coordination" }),
                new SkillData("dualwield", 3, new List<string> { "coordination", "coordination" }),
                new SkillData("recklessness", 3, new List<string> { "strength", "quickness" }),
                new SkillData("sneakattack", 3, new List<string> { "coordination", "quickness" }),
                new SkillData("dirtyfighting", 3, new List<string> { "strength", "coordination" }),
                new SkillData("summoning", 3, new List<string> { "endurance", "self" }),
            };

            return rv;
        }
    }
}