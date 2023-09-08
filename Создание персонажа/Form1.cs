using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Создание_персонажа
{
    public partial class Form1 : Form
    {
        ushort Num;

        sbyte ChosenPerks = 2;
        sbyte ChoosenPerksMax = 2;
        sbyte ChosenMaster = 1;
        sbyte point=27;
        sbyte i;
        byte IndexOfCheckBox = 0;
        sbyte hp; sbyte armor; sbyte speed; sbyte shield; sbyte armorPlus; sbyte hpPlus; sbyte DuelPlus; bool MonchPlus;
        bool more;
        bool OneHanded;


        sbyte[] stat = new sbyte[6] { 8, 8, 8, 8, 8, 8 };
        sbyte[] CheckStat = new sbyte[6] { 10, 10, 10, 10, 10, 10 };
        sbyte[] CheckStatDown = new sbyte[6] { 7, 7, 7, 7, 7, 7 };
        sbyte[] bonus = new sbyte[6] { -1, -1, -1, -1, -1, -1 };
        sbyte[] upCost = new sbyte[6] { 1, 1, 1, 1, 1, 1 };
        sbyte[] raceBonus = new sbyte[8] { 0, 0, 0, 0, 0, 0, 0, 0};
        bool[] weaponSkill = new bool[35];
        CheckBox[] perks = new CheckBox[18];
        CheckBox[] perksMaster = new CheckBox[18];
        Panel MasterPanel = new Panel();
        CheckBox[] boxes = new CheckBox[6];
        ComboBox ChPath = new ComboBox();
        sbyte[] KB = new sbyte[7] { 10, 11, 12, 12, 13, 14, 16 };

        string name;

        string ClassSpec; string RaceSpec; string DragonSpec; string OriginsSpec; string PathSpec;

        string startInventory; string weapon; string[] spells; string PathSpells="";

        public Form1()
        {
            InitializeComponent();
            
            StartPosition = FormStartPosition.CenterScreen;

            StrMinus.Enabled = AgilMinus.Enabled = EndurMinus.Enabled = IntellMinus.Enabled = PercMinus.Enabled = CharMinus.Enabled = false;

            //небольшой костыль из-за проеба первого лейбла
            label9.Location = label8.Location;
                        

            //Делание лейблов полупрозрачными
            label1.BackColor = label2.BackColor = label3.BackColor = label4.BackColor = label5.BackColor = label6.BackColor = label7.BackColor = label9.BackColor
                = label10.BackColor = label11.BackColor = label12.BackColor = label13.BackColor = label14.BackColor = label15.BackColor = label17.BackColor
                = StrBonus.BackColor = AgilBonus.BackColor = EndurBonus.BackColor = IntellBonus.BackColor = PercBonus.BackColor = CharBonus.BackColor
                = ArmorLabel.BackColor = HpLabel.BackColor = label16.BackColor = label18.BackColor = label19.BackColor = label20.BackColor
                = label22.BackColor = Color.FromArgb(128, Color.LightGray);


            for (byte i = 0; i < 3; i++)
            {
                tableLayoutPanel1.RowStyles[i].SizeType = SizeType.AutoSize;
                tableLayoutPanel1.ColumnStyles[i].SizeType = SizeType.AutoSize;
            }

            //настройка кнопок мировозрения
            law_evil.Size = law_neutral.Size = law_good.Size
                = neutral_evil.Size = neutral_neutral.Size = neutral_good.Size
                = chaos_evil.Size = chaos_neutral.Size = chaos_good.Size
                = new Size(tableLayoutPanel1.Width / tableLayoutPanel1.RowCount, tableLayoutPanel1.Height / tableLayoutPanel1.ColumnCount);

            for(byte i = 0; i < weaponSkill.Length; i++)
            {
                weaponSkill[i] = false;
            }

            //флажки навыков
            foreach (var box in perks)
            {
                perks[IndexOfCheckBox] = new CheckBox();
                perks[IndexOfCheckBox].BackColor = Color.FromArgb(128, Color.LightGray);
                perks[IndexOfCheckBox].Size = new Size(125, 19);
                IndexOfCheckBox++;
            }

            perks[0].Click += new EventHandler(AthleticCheck);
            perks[0].Text = "Атлетика";

            perks[1].Click += new EventHandler(AcrobaticCheck);
            perks[1].Text = "Акробатика";

            perks[2].Click += new EventHandler(HandAgilityCheck);
            perks[2].Text = "Ловкость рук";

            perks[3].Click += new EventHandler(SneakCheck);
            perks[3].Text = "Скрытность";

            perks[4].Click += new EventHandler(HistoryCheck);
            perks[4].Text = "История";

            perks[5].Click += new EventHandler(MagicCheck);
            perks[5].Text = "Магия";

            perks[6].Click += new EventHandler(NatureCheck);
            perks[6].Text = "Природа";

            perks[7].Click += new EventHandler(DetectivCheck);
            perks[7].Text = "Расследование";

            perks[8].Click += new EventHandler(ReligionCheck);
            perks[8].Text = "Религия";

            perks[9].Click += new EventHandler(PerceptionCheck);
            perks[9].Text = "Внимание";

            perks[10].Click += new EventHandler(SurviveCheck);
            perks[10].Text = "Выживания";

            perks[11].Click += new EventHandler(TrainingCheck);
            perks[11].Text = "Дресировка";

            perks[12].Click += new EventHandler(MedicineCheck);
            perks[12].Text = "Медицина";

            perks[13].Click += new EventHandler(ProniciatsCheck);
            perks[13].Text = "Проницательность";

            perks[14].Click += new EventHandler(TerrorCheck);
            perks[14].Text = "Запугивание";

            perks[15].Click += new EventHandler(ActingCheck);
            perks[15].Text = "Выступление";

            perks[16].Click += new EventHandler(LieCheck);
            perks[16].Text = "Обман";

            perks[17].Click += new EventHandler(ConvictionCheck);
            perks[17].Text = "Убеждения";

           
            for (byte i = 0; i < perks.Length; i++)
            {
                perksMaster[i] = new CheckBox();
                perksMaster[i].Text = perks[i].Text;
                perksMaster[i].Size = perks[i].Size;
                perksMaster[i].BackColor = perks[i].BackColor;
            }

            perksMaster[4].Click += new EventHandler(HistoryMaster);
            perksMaster[5].Click += new EventHandler(MagicMaster);
            perksMaster[6].Click += new EventHandler(NatureMaster);
            perksMaster[8].Click += new EventHandler(ReligionMaster);
            perksMaster[10].Click += new EventHandler(SurvivalMaster);
            perksMaster[11].Click += new EventHandler(TrainingMaster);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //папка проекта
            if (!Directory.Exists("C:\\D&D Characters"))
                Directory.CreateDirectory("C:\\D&D Characters");


            //выбираемые пути
            ChPath.DropDownStyle = ComboBoxStyle.DropDownList;
            ChPath.Size = new Size(classList.Size.Width - 20, classList.Size.Height);
            ChPath.SelectedIndexChanged += new EventHandler(PathSelect);
            ChPath.Location = new Point(classList.Location.X + 160, classList.Location.Y);


            //панель для мастерства навыков
            MasterPanel.Size = new Size(150, 150);
            MasterPanel.BackColor = Color.Transparent;
            MasterPanel.Location = new Point(ChPath.Location.X, ChPath.Location.Y+20);

            //выбор класса и брони по умолчанию
            RaceBox.SelectedIndex = classList.SelectedIndex = armorList.SelectedIndex = OriginsBox1.SelectedIndex 
                = RightHand.SelectedIndex = DistanceWeapon.SelectedIndex = 0;
            LeftHand.SelectedIndex = 21;
        }

        
        
        //СИЛА

        //плюс параметр
        private void StrPlus_Click(object sender, EventArgs e)
        {
            i = 0;
            StrMinus.Enabled = true;

            ParamsChangePlus(i);

            //обновление данных на экране
            Str.Text = stat[i].ToString();
            Points.Text = point.ToString();
            StrBonus.Text = '(' + bonus[i].ToString() + ')';

            //проверка возможности дальнейшего повышения
            CheckToEnabled();

            WeaponUpdate(sender, e);
        }
        
        //минус параметр
        private void StrMinus_Click(object sender, EventArgs e)
        {
            i = 0;
            StrPlus.Enabled = true;

            ParamsChangeMinus(i);

            //обновление данных на экране
            Str.Text = stat[i].ToString();
            Points.Text = point.ToString();
            StrBonus.Text = '(' + bonus[i].ToString() + ')';


            //проверка возможности дальнейшего повышения
            CheckToEnabled();

            WeaponUpdate(sender, e);
        }


        //ЛОВКОСТЬ

        //плюс параметр
        private void AgilPlus_Click(object sender, EventArgs e)
        {
            i = 1;
            AgilMinus.Enabled = true;

            ParamsChangePlus(i);

            Agil.Text = stat[i].ToString();
            Points.Text = point.ToString();
            AgilBonus.Text = '(' + bonus[i].ToString() + ')';
            IniciativaBox.Text = bonus[1].ToString();

            CheckToEnabled();

            WeaponUpdate(sender, e);
        }

        //минус параметр
        private void AgilMinus_Click(object sender, EventArgs e)
        {
            i = 1;
            AgilPlus.Enabled = true;

            ParamsChangeMinus(i);

            Agil.Text = stat[i].ToString();
            Points.Text = point.ToString();
            AgilBonus.Text = '(' + bonus[i].ToString() + ')';
            AgilMinus.Enabled = stat[i] > 8 + raceBonus[i];
            IniciativaBox.Text = bonus[1].ToString();

            CheckToEnabled();

            WeaponUpdate(sender, e);
        }


        //ВЫНОСЛИВОСТЬ
        private void EndurPlus_Click(object sender, EventArgs e)
        {
            i = 2;
            EndurMinus.Enabled = true;

            ParamsChangePlus(i);

            Endur.Text = stat[i].ToString();
            Points.Text = point.ToString();
            EndurBonus.Text = '(' + bonus[i].ToString() + ')';

            CheckToEnabled();
        }

        private void EndurMinus_Click(object sender, EventArgs e)
        {
            i = 2;
            EndurPlus.Enabled = true;

            ParamsChangeMinus(i);

            Endur.Text = stat[i].ToString();
            Points.Text = point.ToString();
            EndurBonus.Text = '(' + bonus[i].ToString() + ')';

            EndurMinus.Enabled = stat[i] > 8 + raceBonus[i];

            CheckToEnabled();
        }


        //ИНТЕЛЛЕКТ
        private void IntellPlus_Click(object sender, EventArgs e)
        {
            i = 3;
            IntellMinus.Enabled = true;

            ParamsChangePlus(i);

            Intell.Text = stat[i].ToString();
            Points.Text = point.ToString();
            IntellBonus.Text = '(' + bonus[i].ToString() + ')';

            CheckToEnabled();
        }

        private void IntellMinus_Click(object sender, EventArgs e)
        {
            i = 3;
            IntellPlus.Enabled = true;

            ParamsChangeMinus(i);

            Intell.Text = stat[i].ToString();
            Points.Text = point.ToString();
            IntellBonus.Text = '(' + bonus[i].ToString() + ')';

            IntellMinus.Enabled = stat[i] > 8 + raceBonus[i];

            CheckToEnabled();
        }


        // МУДРОСТЬ
        private void PercPlus_Click(object sender, EventArgs e)
        {
            i = 4;
            PercMinus.Enabled = true;

            ParamsChangePlus(i);

            Perc.Text = stat[i].ToString();
            Points.Text = point.ToString();
            PercBonus.Text = '(' + bonus[i].ToString() + ')';

            CheckToEnabled();

            armorList_SelectedIndexChanged(sender, e);
        }

        private void PercMinus_Click(object sender, EventArgs e)
        {
            i = 4;
            PercPlus.Enabled = true;

            ParamsChangeMinus(i);

            Perc.Text = stat[i].ToString();
            Points.Text = point.ToString();
            PercBonus.Text = '(' + bonus[i].ToString() + ')';

            PercMinus.Enabled = stat[i] > 8 + raceBonus[i];

            CheckToEnabled();

            armorList_SelectedIndexChanged(sender, e);
        }


        //ХАРИЗМА
        private void CharPlus_Click(object sender, EventArgs e)
        {
            i = 5;
            CharMinus.Enabled = true;

            ParamsChangePlus(i);

            Chr.Text = stat[i].ToString();
            Points.Text = point.ToString();
            CharBonus.Text = '(' + bonus[i].ToString() + ')';

            CheckToEnabled();
        }

        private void CharMinus_Click(object sender, EventArgs e)
        {
            i = 5;
            CharPlus.Enabled = true;

            ParamsChangeMinus(i);

            Chr.Text = stat[i].ToString();
            Points.Text = point.ToString();
            CharBonus.Text = '(' + bonus[i].ToString() + ')';

            CharMinus.Enabled = stat[i] > 8 + raceBonus[i];
            CheckToEnabled();
        }
        

        //Изменение параметров
        //ПЛЮС
        private void ParamsChangePlus(sbyte i)
        {
            //повышение
            if (point >= upCost[i])
            {
                stat[i]++;
                point -= upCost[i];
            }

            //увеличение модификатора
            if (stat[i] == CheckStat[i])
            {
                bonus[i]++;
                CheckStatDown[i] = CheckStat[i];
                CheckStatDown[i]--;
                CheckStat[i] += 2;
            }

            //повышение стоимости повышения
            if (stat[i] == 13 + raceBonus[i])
            {
                upCost[i] = 2;
            }
        }

        //МИНУС
        private void ParamsChangeMinus(sbyte i)
        {
            stat[i]--;

            if (stat[i] == 12 + raceBonus[i])
            {
                upCost[i] = 1;
            }

            if (stat[i] == CheckStatDown[i])
            {
                bonus[i]--;
                CheckStat[i] = CheckStatDown[i];
                CheckStat[i]++;
                CheckStatDown[i] -= 2;
            }

            point += upCost[i];
        }


        // ВКЛ/ВЫКЛ стрелок
        private void CheckToEnabled()
        {
            StrPlus.Enabled = stat[0] < 15 + raceBonus[0] && point >= upCost[0];
            AgilPlus.Enabled = stat[1] < 15 + raceBonus[1] && point >= upCost[1];
            EndurPlus.Enabled = stat[2] < 15 + raceBonus[2] && point >= upCost[2];
            IntellPlus.Enabled = stat[3] < 15 + raceBonus[3] && point >= upCost[3];
            PercPlus.Enabled = stat[4] < 15 + raceBonus[4] && point >= upCost[4];
            CharPlus.Enabled = stat[5] < 15 + raceBonus[5] && point >= upCost[5];
        }



        //ПЕРСОНАЛИЗАЦИЯ

        //выбор расы
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label9.Text = "Раса: " + RaceBox.SelectedItem.ToString();
            
            //сброс бонусов прежней расы
            hp -= raceBonus[6];
            raceBonus[6] = 0;
            point = 27;
            ChoosenPerksMax -= raceBonus[7];
            raceBonus[7] = 0;

            for (byte i = 0; i < weaponSkill.Length; i++)
            {
                weaponSkill[i] = false;
            }
            RaceSpec = "";

            Controls.Remove(DragonType);
            ResultPanel.Location = new Point(5, 150);

            for (byte i = 0; i < stat.Length; i++)
            {
                stat[i] = 8;
                CheckStat[i] = 10;
                CheckStatDown[i] = 7;
                upCost[i] = 1;
                bonus[i] = -1;
            }

            StrMinus.Enabled = AgilMinus.Enabled = EndurMinus.Enabled = IntellMinus.Enabled = PercMinus.Enabled = CharMinus.Enabled = false;
            StrPlus.Enabled = AgilPlus.Enabled = EndurPlus.Enabled = IntellPlus.Enabled = PercPlus.Enabled = CharPlus.Enabled = true;

            
            //выдача бонусов
            switch (RaceBox.SelectedIndex)
            {
                case 0:
                    for (byte i = 0; i < stat.Length; i++)
                    {
                        raceBonus[i] = 1;
                    }
                    speed = 30;
                    break;

                //ЭЛЬФЫ
                case 1:
                    raceBonus[0] = 0;
                    raceBonus[1] = 2;
                    raceBonus[2] = 0;
                    raceBonus[3] = 0;
                    raceBonus[4] = 0;
                    raceBonus[5] = 0;
                    speed = 30;
                    point = 29;
                    raceBonus[7] = 2;
                    break;

                case 2:
                    raceBonus[0] = 0;
                    raceBonus[1] = 2;
                    raceBonus[2] = 0;
                    raceBonus[3] = 0;
                    raceBonus[4] = 1;
                    raceBonus[5] = 0;
                    speed = 35;
                    weaponSkill[9] = weaponSkill[16] = weaponSkill[17] = weaponSkill[22] = true;
                    RaceSpec = "Быстрые ноги: Базовая скорость передвижения увеличена до 35 футов." + Environment.NewLine + Environment.NewLine + "Темновидение: Привыкшие к темным лесам и ночному небу, эльфы имеют исключительное зрение в темноте и условиях плохой видимости. При тусклом освещении, вы видите на расстоянии 60 футов так, словно при ярком свете, а в темноте так, словно при тусклом освещении. Однако вы не можете различать цвета в темноте, только оттенки серого."
                                + Environment.NewLine + Environment.NewLine + "Обостренные чувства: Вы обладаете умением Восприятие."
                                + Environment.NewLine + Environment.NewLine + "Волшебное происхождение: Вы обладаете преимуществом при спасбросках против очарования, кроме того вас невозможно усыпить магией." + Environment.NewLine + Environment.NewLine +
                                "Транс: Эльфы не нуждаются во сне. Вместо этого они погружаются в глубокую медитацию, находясь в полубессознательном состоянии на протяжении 4 часов в день. В процессе медитации, вам в некотором роде могут сниться сны, которые на деле являются умственными упражнениями. Подобная медитация равносильна 8 часам сна у человека.";
                    break;

                case 3:
                    raceBonus[0] = 0;
                    raceBonus[1] = 2;
                    raceBonus[2] = 0;
                    raceBonus[3] = 1;
                    raceBonus[4] = 0;
                    raceBonus[5] = 0;
                    weaponSkill[9] = weaponSkill[16] = weaponSkill[17] = weaponSkill[22] = true;
                    RaceSpec = "Темновидение: Привыкшие к темным лесам и ночному небу, эльфы имеют исключительное зрение в темноте и условиях плохой видимости. При тусклом освещении, вы видите на расстоянии 60 футов так, словно при ярком свете, а в темноте так, словно при тусклом освещении. Однако вы не можете различать цвета в темноте, только оттенки серого."
                                + Environment.NewLine + Environment.NewLine + "Обостренные чувства: Вы обладаете умением Восприятие."
                                + Environment.NewLine + Environment.NewLine + "Владение эльфийским оружием. Вы владеете длинным мечом, коротким мечом, коротким луком и длинным луком."
                                + Environment.NewLine + Environment.NewLine + "Трюк. Вы знаете один из трюков, находящихся в списке заклинаний волшебника. Вашим параметром для этого заклинания является Интеллект."
                                + Environment.NewLine + Environment.NewLine + "Дополнительный язык. Вы можете разговаривать, читать и писать на одном дополнительном языке на ваш выбор."
                                + Environment.NewLine + Environment.NewLine + "Волшебное происхождение: Вы обладаете преимуществом при спасбросках против очарования, кроме того вас невозможно усыпить магией." + Environment.NewLine + Environment.NewLine +
                                "Транс: Эльфы не нуждаются во сне. Вместо этого они погружаются в глубокую медитацию, находясь в полубессознательном состоянии на протяжении 4 часов в день. В процессе медитации, вам в некотором роде могут сниться сны, которые на деле являются умственными упражнениями. Подобная медитация равносильна 8 часам сна у человека.";
                    speed = 30;
                    break;

                case 4:
                    raceBonus[0] = 0;
                    raceBonus[1] = 2;
                    raceBonus[2] = 0;
                    raceBonus[3] = 0;
                    raceBonus[4] = 0;
                    raceBonus[5] = 1;
                    weaponSkill[17] = weaponSkill[19] = weaponSkill[23] = true;
                    RaceSpec = "Обостренные чувства: Вы обладаете умением Восприятие."
                                + Environment.NewLine + Environment.NewLine +
                                "Исключительное темновидение. Ваш навык темновидения имеет радиус 120 футов."
                                + Environment.NewLine + Environment.NewLine + "Чувствительность к солнцу. Вы получаете помеху, когда совершаете броски атаки или проверки Мудрости(Восприятие), которые полагаются на зрение, если вы или цель вашей атаки находится под прямыми солнечными лучами."
                                + Environment.NewLine + Environment.NewLine + "Магия Дроу. Вы знаете заклинание танцующие огоньки. По достижению 3 уровня, вы можете использовать заклинание волшебный огонь 1 раз в день. На 5 уровне вы можете использовать заклинание тьма один раз в день. Используйте параметр Харизмы для этих заклинаний."
                                + Environment.NewLine + Environment.NewLine + "Владение оружием Дроу. Вы владеете рапирами, короткими мечами и ручными арбалетами."
                                + Environment.NewLine + Environment.NewLine + "Волшебное происхождение: Вы обладаете преимуществом при спасбросках против очарования, кроме того вас невозможно усыпить магией." + Environment.NewLine + Environment.NewLine +
                                "Транс: Эльфы не нуждаются во сне. Вместо этого они погружаются в глубокую медитацию, находясь в полубессознательном состоянии на протяжении 4 часов в день. В процессе медитации, вам в некотором роде могут сниться сны, которые на деле являются умственными упражнениями. Подобная медитация равносильна 8 часам сна у человека.";
                    speed = 30;
                    break;

                //ДВАРФЫ
                case 5:
                    raceBonus[0] = 2;
                    raceBonus[1] = 0;
                    raceBonus[2] = 2;
                    raceBonus[3] = 0;
                    raceBonus[4] = 0;
                    raceBonus[5] = 0;
                    speed = 25;
                    weaponSkill[6] = weaponSkill[8] = weaponSkill[11] = weaponSkill[12] = true;
                    RaceSpec = "Знание камня: Когда вам необходимо сделать проверку Интеллекта (История) касательно происхождения каменной кладки, считается, что вы владеете умением История и добавляете к проверке двукратный бонус мастерства вместо обычного."
                        + Environment.NewLine + Environment.NewLine + "Владение доспехами: Вы владеете легкими и средними доспехами" + Environment.NewLine + Environment.NewLine + "Дворфийская стойкость; Дворфы имеют преимуществопри спасбросках против яда, а также сопротивление урону от яда" + Environment.NewLine + Environment.NewLine +
                        "Темновидение: Привыкшие жить под землей, дворфы имеют исключительное зрение в темноте и условиях плохой видимости. При тусклом освещении вы видите на расстоянии 60 футов так, словно при ярком свете, а в темноте так, словно при тусклом освещении.Однако вы неможете различать цвета в темноте, только оттенки серого."
                        + Environment.NewLine + Environment.NewLine + "Боевая тренировка. Дворфы свободно владеют боевым топором, топориками, легким молотом и боевым молотом";

                    break;

                case 6:
                    raceBonus[0] = 0;
                    raceBonus[1] = 0;
                    raceBonus[2] = 2;
                    raceBonus[3] = 0;
                    raceBonus[4] = 1;
                    raceBonus[5] = 0;
                    raceBonus[6] = 1;
                    weaponSkill[6] = weaponSkill[8] = weaponSkill[11] = weaponSkill[12] = true;
                    RaceSpec = "Знание камня: Когда вам необходимо сделать проверку Интеллекта (История) касательно происхождения каменной кладки, считается, что вы владеете умением История и добавляете к проверке двукратный бонус мастерства вместо обычного."
                        + Environment.NewLine + Environment.NewLine + "Дворфийская крепкость: Ваши очки здоровья увеличиваются на 1 за каждый уровень." + Environment.NewLine + Environment.NewLine + "Дворфийская стойкость; Дворфы имеют преимуществопри спасбросках против яда, а также сопротивление урону от яда" + Environment.NewLine + Environment.NewLine +
                        "Темновидение: Привыкшие жить под землей, дворфы имеют исключительное зрение в темноте и условиях плохой видимости. При тусклом освещении вы видите на расстоянии 60 футов так, словно при ярком свете, а в темноте так, словно при тусклом освещении.Однако вы неможете различать цвета в темноте, только оттенки серого."
                        + Environment.NewLine + Environment.NewLine + "Боевая тренировка. Дворфы свободно владеют боевым топором, топориками, легким молотом и боевым молотом";
                    speed = 25;
                    break;

                //ГНОМЫ
                case 7:
                    raceBonus[0] = 0;
                    raceBonus[1] = 0;
                    raceBonus[2] = 1;
                    raceBonus[3] = 2;
                    raceBonus[4] = 0;
                    raceBonus[5] = 0;
                    speed = 25;
                    RaceSpec = "Знание ремёсел: Если вам необходимо сделать проверку Интеллекта (История) касательно магических предметов, алхимических объектов или технических устройств, добавьте к броску двукратный бонус мастерства вместо обычного";
                    break;

                case 8:
                    raceBonus[0] = 0;
                    raceBonus[1] = 1;
                    raceBonus[2] = 0;
                    raceBonus[3] = 2;
                    raceBonus[4] = 0;
                    raceBonus[5] = 0;
                    RaceSpec = "Прирожденный иллюзионист: Вы знаете заклинание малая иллюзия(параметр - Интеллект)";
                    speed = 25;
                    break;


                //ПОЛУРОСЛИКИ
                case 9:
                    raceBonus[0] = 0;
                    raceBonus[1] = 2;
                    raceBonus[2] = 0;
                    raceBonus[3] = 0;
                    raceBonus[4] = 0;
                    raceBonus[5] = 1;
                    RaceSpec = "Удачливый: Если вы выбросили 1 при броске атаки, проверке навыка или спасброске, вы можете перебросить кубик и использовать новый результат."
                                    + Environment.NewLine + Environment.NewLine + "Отважный: У вас есть преимущество при спасбросках против эффектов страха."
                                    + Environment.NewLine + Environment.NewLine + "Проворство Халфлингов: Вы можете двигаться через место, где находится существо больше вас размером."
                                    + Environment.NewLine + Environment.NewLine + "Врожденная скрытность: Вы можете попытаться спрятаться, находясь за любым существом, которое хотя бы на 1 размер больше вас.";
                    speed = 25;
                    break;

                case 10:
                    raceBonus[0] = 0;
                    raceBonus[1] = 2;
                    raceBonus[2] = 1;
                    raceBonus[3] = 0;
                    raceBonus[4] = 0;
                    raceBonus[5] = 0;
                    RaceSpec = "Удачливый: Если вы выбросили 1 при броске атаки, проверке навыка или спасброске, вы можете перебросить кубик и использовать новый результат."
                                + Environment.NewLine + Environment.NewLine + "Отважный: У вас есть преимущество при спасбросках против эффектов страха."
                                + Environment.NewLine + Environment.NewLine + "Проворство Халфлингов: Вы можете двигаться через место, где находится существо больше вас размером."
                                + Environment.NewLine + Environment.NewLine + "Стойкость. У вас есть преимущество при спасбросках против яда, а также сопротивление урону от яда.";

                    speed = 25;
                    break;


                //ОСТАЛЬНЫЕ 
                case 11:
                    raceBonus[0] = 0;
                    raceBonus[1] = 0;
                    raceBonus[2] = 0;
                    raceBonus[3] = 1;
                    raceBonus[4] = 0;
                    raceBonus[5] = 2;
                    speed = 30;
                    break;

                case 12:
                    raceBonus[0] = 2;
                    raceBonus[1] = 0;
                    raceBonus[2] = 1;
                    raceBonus[3] = 0;
                    raceBonus[4] = 0;
                    raceBonus[5] = 0;
                    perks[14].Checked = true;
                    perks[14].Enabled = false;
                    speed = 30;
                    break;

                case 13:
                    raceBonus[0] = 2;
                    raceBonus[1] = 0;
                    raceBonus[2] = 0;
                    raceBonus[3] = 0;
                    raceBonus[4] = 0;
                    raceBonus[5] = 1;
                    ResultPanel.Location = new Point(5, 180);
                    Controls.Add(DragonType);
                    speed = 30;
                    break;
            }

            //суммирование характеристик и бонуса расы
            for (byte i = 0; i < stat.Length; i++)
            {
                stat[i] += raceBonus[i];
                while (stat[i] >= CheckStat[i])
                {
                    bonus[i]++;
                    CheckStatDown[i] = (sbyte)(CheckStat[i] - 1);
                    CheckStat[i] += 2;
                }
                while (stat[i] <= CheckStatDown[i])
                {
                    bonus[i]--;
                    CheckStat[i] = (sbyte)(CheckStatDown[i] + 1);
                    CheckStatDown[i] -= 2;
                }
            }
            ChoosenPerksMax += raceBonus[7];
            ChosenPerks = ChoosenPerksMax;

            //обновление окна
            Str.Text = stat[0].ToString();
            Agil.Text = stat[1].ToString();
            Endur.Text = stat[2].ToString();
            Intell.Text = stat[3].ToString();
            Perc.Text = stat[4].ToString();
            Chr.Text = stat[5].ToString();
            Points.Text = point.ToString();
            SpeedBox.Text = speed.ToString();
            label21.Text = ChosenPerks.ToString();

            StrBonus.Text = '(' + bonus[0].ToString() + ')';
            AgilBonus.Text = '(' + bonus[1].ToString() + ')';
            EndurBonus.Text = '(' + bonus[2].ToString() + ')';
            IntellBonus.Text = '(' + bonus[3].ToString() + ')';
            PercBonus.Text = '(' + bonus[4].ToString() + ')';
            CharBonus.Text = '(' + bonus[5].ToString() + ')';

            IniciativaBox.Text = bonus[1].ToString();

            hp += raceBonus[6];
            classList_SelectedIndexChanged(sender, e);
        }

        //выбор типа драконорожденных
        private void DragonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DragonType.SelectedIndex)
            {
                case 0:
                    DragonSpec = "";
                    break;

                case 1:
                    DragonSpec = "";
                    break;

                case 2:
                    DragonSpec = "";
                    break;

                case 3:
                    DragonSpec = "";
                    break;

                case 4:
                    DragonSpec = "";
                    break;

                case 5:
                    DragonSpec = "";
                    break;

                case 6:
                    DragonSpec = "";
                    break;

                case 7:
                    DragonSpec = "";
                    break;

                case 8:
                    DragonSpec = "";
                    break;

                case 9:
                    DragonSpec = "";
                    break;
            }
        }
        
        //выбор класса
        private void classList_SelectedIndexChanged(object sender, EventArgs e)
        {
            label11.Text = "Класс: " + classList.SelectedItem.ToString();

            ChPath.Items.Clear();
            Controls.Remove(ChPath);

            foreach (CheckBox box in perks)
            {
                PerksPanel.Controls.Remove(box);
                box.Checked = false;
            }

            RaceSkillSave();

            switch (classList.SelectedIndex)
            {
                case 0:
                    hp = 12;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[0], perks[6], perks[9], perks[10], perks[11], perks[14] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChosenPerks = ChoosenPerksMax = (sbyte)(2 + raceBonus[7]);

                    for (byte i = 0; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = true;
                    }
                    break;

                case 1:
                    hp = 8;
                    ClassSpec = "";
                    foreach (CheckBox box in perks)
                        PerksPanel.Controls.Add(box);

                    for (byte i = 0; i < perks.Length; i++)
                    {
                        perks[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(3 + raceBonus[7]);

                    for (byte i = 0; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = i < 11 || i == 16 || i == 17 || i == 19 || i == 23 || i == 25 || i == 26;
                    }
                    break;

                case 2:
                    hp = 8;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[4], perks[8], perks[12], perks[13], perks[17] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(2 + raceBonus[7]);
                    for (byte i = 0; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = i < 11 || i == 25 || i == 26;
                    }
                    ChPath.Items.AddRange(new object[] { "Домен Знания", "Домен Жизни", "Домен Света", "Домен Природы", "Домен Бури", "Домен Обмана", "Домен Войны" });
                    Controls.Add(ChPath);
                    ChPath.SelectedIndex = 0;
                    break;

                case 3:
                    hp = 8;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[5], perks[6], perks[8], perks[9], perks[10], perks[11], perks[12], perks[13] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(2 + raceBonus[7]);

                    weaponSkill[0] = weaponSkill[1] = weaponSkill[3] = weaponSkill[4] = weaponSkill[5] = weaponSkill[7] = weaponSkill[20] = weaponSkill[25] = weaponSkill[26] = true;
                    break;

                case 4:
                    hp = 10;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[0], perks[1], perks[4], perks[9], perks[10], perks[11], perks[13], perks[14] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(2 + raceBonus[7]);

                    for (byte i = 0; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = true;
                    }

                    ChPath.Items.AddRange(new object[] { "Стрельба", "Оборона", "Дуэльянт", "Двуручное оружие", "Защита", "Бой двумя оружиями" });
                    Controls.Add(ChPath);
                    ChPath.SelectedIndex = 0;
                    break;

                case 5:
                    hp = 8;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[0], perks[1], perks[3], perks[4], perks[8], perks[13] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(2 + raceBonus[7]);

                    for (byte i = 0; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = i < 11 || i == 17 || i == 25 || i == 26;
                    }
                    break;

                case 6:
                    hp = 10;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[0], perks[8], perks[12], perks[13], perks[14], perks[17] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(2 + raceBonus[7]);

                    for (byte i = 0; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = true;
                    }
                    break;

                case 7:
                    hp = 10;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[0], perks[3], perks[6], perks[7], perks[9], perks[10], perks[11], perks[13] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(3 + raceBonus[7]);

                    for (byte i = 0; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = true;
                    }
                    break;

                case 8:
                    hp = 8;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[0], perks[1], perks[2], perks[3], perks[7], perks[9], perks[13], perks[14], perks[15], perks[16], perks[17] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(4 + raceBonus[7]);

                    for (byte i = 0; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = i < 11 || i == 16 || i == 17 || i == 19 || i == 23 || i == 25 || i == 26;
                    }
                    break;

                case 9:
                    hp = 6;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[5], perks[8], perks[13], perks[14], perks[16], perks[17] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(2 + raceBonus[7]);

                    weaponSkill[0] = weaponSkill[4] = weaponSkill[10] = weaponSkill[26] = true;

                    ChPath.Items.AddRange(new object[] { "Драконий предок", "Всплеск магии" });
                    Controls.Add(ChPath);
                    ChPath.SelectedIndex = 0;
                    break;

                case 10:
                    hp = 8;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[4], perks[5], perks[6], perks[7], perks[8], perks[14], perks[16] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(2 + raceBonus[7]);

                    for (byte i = 0; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = i < 11 || i == 25 || i == 26;
                    }

                    ChPath.Items.AddRange(new object[] { "Правитель фей", "Владыка демонов", "Древнейший" });
                    Controls.Add(ChPath);
                    ChPath.SelectedIndex = 0;
                    break;

                case 11:
                    hp = 6;
                    ClassSpec = "";
                    boxes = new CheckBox[] { perks[4], perks[5], perks[7], perks[8], perks[12], perks[13] };

                    for (byte i = 0; i < boxes.Length; i++)
                    {
                        boxes[i].Location = new Point(10, 5 + (20 * i));
                    }
                    ChoosenPerksMax = ChosenPerks = (sbyte)(2 + raceBonus[7]);

                    weaponSkill[0] = weaponSkill[4] = weaponSkill[10] = weaponSkill[26] = true;
                    break;
            }

            weaponSkill[17] = RaceBox.SelectedIndex == 2 || RaceBox.SelectedIndex == 3 || RaceBox.SelectedIndex == 4 || weaponSkill[17];
            weaponSkill[9] = RaceBox.SelectedIndex == 2 || RaceBox.SelectedIndex == 3 || weaponSkill[9];
            weaponSkill[16] = RaceBox.SelectedIndex == 2 || RaceBox.SelectedIndex == 3 || weaponSkill[16];
            weaponSkill[22] = RaceBox.SelectedIndex == 2 || RaceBox.SelectedIndex == 3 || weaponSkill[22];
            weaponSkill[19] = RaceBox.SelectedIndex == 4 || weaponSkill[19];
            weaponSkill[23] = RaceBox.SelectedIndex == 4 || weaponSkill[23];

            hp += bonus[2];
            PerksPanel.Controls.AddRange(boxes);
            HpBox.Text = hp.ToString();
            label21.Text = ChosenPerks.ToString();
            armorList_SelectedIndexChanged(sender, e);
            OriginsBox1_SelectedIndexChanged(sender, e);
            WeaponUpdate(sender, e);
        }
        
        private void PathSelect(object sender, EventArgs e)
        {
            hp -= hpPlus;
            hpPlus = 0;
            CheckBox[] checks = new CheckBox[4];
            
            foreach(CheckBox check in perksMaster)
                MasterPanel.Controls.Remove(check);

            for(byte i = 0; i < perksMaster.Length; i++)
            {
                if (perksMaster[i].Checked)
                {
                    perks[i].Checked = false;
                    perks[i].Enabled = true;
                }

                perksMaster[i].Checked = false;
                perksMaster[i].Enabled = true;
            }
            Controls.Remove(MasterPanel);
           
            switch (ChPath.SelectedItem)
            {
                //КЛИРИК
                case "Домен Знания":
                    PathSpells = "приказ, оценка";
                    checks = new CheckBox[] { perksMaster[4], perksMaster[5], perksMaster[6], perksMaster[8] };
                    Controls.Add(MasterPanel);
                    ChosenMaster = 2;
                    break;

                case "Домен Жизни":
                    PathSpells = "благословение, исцеление ран";
                    break;

                case "Домен Света":
                    PathSpells = "пылающие руки, волшебный огонь";
                    break;

                case "Домен Природы":
                    PathSpells = "дружба с животными, общение с животными";
                    checks = new CheckBox[] { perksMaster[6], perksMaster[10], perksMaster[11] };
                    Controls.Add(MasterPanel);
                    ChosenMaster = 1;
                    break;

                case "Домен Бури":
                    PathSpells = "облако тумана, громовая волна";
                    for(byte i = 11; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = true;
                    }
                    break;

                case "Домен Обмана":
                    PathSpells = "очаровать персону, маскировка";
                    break;

                case "Домен Войны":
                    PathSpells = "божественное покровительство, щит веры";
                    for (byte i = 11; i < weaponSkill.Length; i++)
                    {
                        weaponSkill[i] = true;
                    }
                    break;


                //ВОИН
                case "Стрельба":
                    DistanceWeapon_SelectedIndexChanged(sender, e);
                    break;

                case "Оборона":

                    break;

                case "Дуэльянт":
                    RightHand_SelectedIndexChanged(sender, e);
                    break;

                case "Двуручное оружие":

                    break;

                case "Защита":
                    DistanceWeapon_SelectedIndexChanged(sender, e);
                    break;

                case "Бой двумя оружиями":

                    break;


                //КОЛДУН
                case "Драконий предок":
                    hpPlus = 1;
                    DistanceWeapon_SelectedIndexChanged(sender, e);
                    break;

                case "Всплеск магии":

                    break;


                //ЧЕРНОКНИЖНИК
                case "Правитель фей":
                    PathSpells = "волшебный огонь, сон";
                    break;

                case "Владыка демонов":
                    PathSpells = "горящие руки, приказ";
                    break;

                case "Древнейший":
                    PathSpells = "шепот диссонанса, истеричный смех Таши";
                    break;
            }

            try
            {
                for (byte i = 0; i < checks.Length; i++)
                    checks[i].Location = new Point(5, 5 + (20 * i));

                MasterPanel.Controls.AddRange(checks);
            }
            catch
            {

            }
            
            hp += hpPlus;
            HpBox.Text = hp.ToString();
            OrgiginsSelect(sender, e);
        }

        //выбор происхождения
        private void OriginsBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label16.Text = "Происхождение: " + OriginsBox1.SelectedItem.ToString();

            ChosenPerks = ChoosenPerksMax;
            label21.Text = ChosenPerks.ToString();

            for (byte i = 0; i < perks.Length; i++)
            {
                perks[i].Checked = false;
                perks[i].Enabled = true;
            }

            RaceSkillSave();

            OrgiginsSelect(sender, e);
        }

        //switch происхождений
        private void OrgiginsSelect(object sender, EventArgs e)
        {
            switch (OriginsBox1.SelectedIndex)
            {
                case 0:
                    perks[8].Checked = perks[13].Checked = true;
                    perks[8].Enabled = perks[13].Enabled = false;
                    startInventory = "Простая одежда, кошель с 15 золотыми, книга молитв, риза, 5 палочек фимиама, священный символ";
                    break;

                case 1:
                    perks[2].Checked = perks[16].Checked = true;
                    perks[2].Enabled = perks[16].Enabled = false;
                    startInventory = "Дорогая одежда, кошель с 15 золотыми, набор для маскировки, один предмет для мошеничества(утяжеленные кубики, помеченные карты или баночки с разноцветной жидкостью )";
                    break;

                case 2:
                    perks[3].Checked = perks[16].Checked = true;
                    perks[3].Enabled = perks[16].Enabled = false;
                    startInventory = "Темная одежда с капюшоном, кошель с 15 золотыми, монтировка";
                    break;

                case 3:
                    perks[1].Checked = perks[15].Checked = true;
                    perks[1].Enabled = perks[15].Enabled = false;
                    startInventory = "Костюм, кошель с 15 золотыми, музыкальный инструмент(на ваш выбор), безделушка от поклоника";
                    break;

                case 4:
                    perks[10].Checked = perks[11].Checked = true;
                    perks[10].Enabled = perks[11].Enabled = false;
                    startInventory = "Простая одежда, кошель с 10 золотыми, котелок, инструменты ремеслинка(на ваш выбор)";
                    break;

                case 5:
                    perks[17].Checked = perks[13].Checked = true;
                    perks[17].Enabled = perks[13].Enabled = false;
                    startInventory = "Одежда путника, кошель с 15 золотыми, набор ремесленика(на ваш выбор), рекомендательное письмо вашей гильдии";
                    break;

                case 6:
                    perks[8].Checked = perks[12].Checked = true;
                    perks[8].Enabled = perks[12].Enabled = false;
                    startInventory = "Простая одежда, кошель с 5 золотыми, зимнее одеяло, набор травника, футляр с вашими исследованиями";
                    break;

                case 7:
                    perks[17].Checked = perks[4].Checked = true;
                    perks[17].Enabled = perks[4].Enabled = false;
                    startInventory = "Дорогая одежда, кошель с 25 золотыми, фамильное кольцо, свиток с родословной";
                    break;

                case 8:
                    perks[0].Checked = perks[10].Checked = true;
                    perks[0].Enabled = perks[10].Enabled = false;
                    startInventory = "Одежда путника, кошель с 10 золотыми, капкан, посох, трофей с убитого животного";
                    break;

                case 9:
                    perks[4].Checked = perks[5].Checked = true;
                    perks[4].Enabled = perks[5].Enabled = false;
                    startInventory = "Простая одежда, кошель с 10 золотыми, перо, бутылочка чернил, маленький нож, письмо коллеги с вопросом, на который у вас нет ответа";
                    break;

                case 10:
                    perks[0].Checked = perks[9].Checked = true;
                    perks[0].Enabled = perks[9].Enabled = false;
                    startInventory = "Простая одежда, кошель с 10 золотыми, 50 футов шелковой веревки, счастливый талисман, дубина";
                    break;

                case 11:
                    perks[0].Checked = perks[14].Checked = true;
                    perks[0].Enabled = perks[14].Enabled = false;
                    startInventory = "Простая одежда, кошель с 10 золотыми, набор костей или карт, военные награды";
                    break;

                case 12:
                    perks[2].Checked = perks[3].Checked = true;
                    perks[2].Enabled = perks[3].Enabled = false;
                    startInventory = "Простая одежда, кошель с 10 золотыми, ручная мышь, маленький нож, памятный значок, карта родного города";
                    break;

            }
        }



        //МИРОВОЗРЕНИЯ

        //ЗАКОННЫЕ
        private void law_good_Click(object sender, EventArgs e)
        {
            label17.Text = "Мировозрение: Законопослушный добрый";
        }

        private void law_neutral_Click(object sender, EventArgs e)
        {
            label17.Text = "Мировозрение: Законопослушный нейтральный";
        }

        private void law_evil_Click(object sender, EventArgs e)
        {
            label17.Text = "Мировозрение: Законопослушный злой";
        }

        //НЕЙТРАЛ
        private void neutral_good_Click(object sender, EventArgs e)
        {
            label17.Text = "Мировозрение: Нейтральный добрый";
        }

        private void neutral_neutral_Click(object sender, EventArgs e)
        {
            label17.Text = "Мировозрение: Истинно нейтральный";
        }

        private void neutral_evil_Click(object sender, EventArgs e)
        {
            label17.Text = "Мировозрение: Нейтральный злой";
        }

        //ХАОС
        private void chaos_good_Click(object sender, EventArgs e)
        {
            label17.Text = "Мировозрение: Хаотически добрый";
        }

        private void chaos_neutral_Click(object sender, EventArgs e)
        {
            label17.Text = "Мировозрение: Хаотически нейтральный";
        }

        private void chaos_evil_Click(object sender, EventArgs e)
        {
            label17.Text = "Мировозрение: Хаотически злой";
        }


        
        //СНАРЯЖЕНИЕ
        //выбор брони
        private void armorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            label14.Text = $"Броня: {armorList.SelectedItem.ToString()}";
            armor-= armorPlus;
            armorPlus =0;

            if (armorList.SelectedIndex == 0 && classList.SelectedIndex == 0)
                armor = (sbyte)(KB[armorList.SelectedIndex] + bonus[1] + bonus[2]+shield);
            else
            if (armorList.SelectedIndex < 3)
            {
                armor = (sbyte)(KB[armorList.SelectedIndex] + bonus[1] + shield);
            }
            else if (armorList.SelectedIndex < 4)
            {
                if (bonus[1] < 3)
                    armor = (sbyte)(KB[armorList.SelectedIndex] + bonus[1] + shield);
                else
                    armor = (sbyte)(KB[armorList.SelectedIndex] + 2 + shield);
            }
            else
                armor = (sbyte)(KB[armorList.SelectedIndex] + shield);

            if (armorList.SelectedIndex == 0 && ChPath.SelectedIndex == 0 && classList.SelectedIndex == 9 )
                armor = (sbyte)(13 + bonus[1] + shield);

            if (armorList.SelectedIndex == 0 && classList.SelectedIndex == 5 && LeftHand.SelectedIndex != 16)
                armor = (sbyte)(KB[armorList.SelectedIndex] + bonus[1] + bonus[4]);

            if (ChPath.SelectedIndex == 1 && classList.SelectedIndex == 4 && armorList.SelectedIndex != 0)
                armorPlus = 1;

            
            armor+=armorPlus;
            ArmorBox.Text = armor.ToString();
        }

        //оружие
        private void RightHand_SelectedIndexChanged(object sender, EventArgs e)
        {
            LeftHand.Enabled = true;

            RightAtt.Text = RightDice.Text = "";

            MonchPlus = armorList.SelectedIndex == 0 && classList.SelectedIndex == 5 && LeftHand.SelectedIndex != 16;

            if (classList.SelectedIndex == 4 && ChPath.SelectedIndex == 2 && LeftHand.SelectedIndex == 21 && OneHanded) 
                DuelPlus = 2;
            else
                DuelPlus = 0;

            switch (RightHand.SelectedIndex)
            {
                case 0:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[0])
                            RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                        else
                            RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                        RightDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[0])
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        RightDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    OneHanded = true;
                    break;

                case 1:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[1])
                            RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                        else
                            RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                        RightDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[1])
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        RightDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    OneHanded = true;
                    break;

                case 2:
                    if (weaponSkill[2])
                        RightAtt.Text = (bonus[0] + 2).ToString();
                    else
                        RightAtt.Text = (bonus[0]).ToString();

                    RightDice.Text = $"d8 + {bonus[0]}";

                    LeftHand.SelectedIndex = 21;
                    LeftHand.Enabled = OneHanded = false;
                    break;

                case 3:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[3])
                            RightAtt.Text = (bonus[0] + DuelPlus + 2).ToString();
                        else
                            RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                        RightDice.Text = $"d4 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[3])
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        RightDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";
                    }
                    OneHanded = true;
                    break;

                case 4:
                    if (weaponSkill[4])
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2 + DuelPlus).ToString() : (bonus[0] + DuelPlus + 2).ToString();
                    else
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + DuelPlus).ToString() : (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";
                    OneHanded = true;
                    //f
                    break;

                case 5:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[5])
                            RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                        else
                            RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                        RightDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[5])
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        RightDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    OneHanded = true;
                    break;

                case 6:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[6])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d4 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[6])
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        RightDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";
                    }
                    OneHanded = true;
                    break;

                case 7:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[7])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[7])
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        RightDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    OneHanded = true;
                    break;

                case 8:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[8])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[8])
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        RightDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    OneHanded = true;
                    break;

                case 9:
                    if (weaponSkill[11])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d8 + {bonus[0]}";
                    break;

                case 10:
                    if (weaponSkill[12])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d8 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 11:
                    if (weaponSkill[13])
                        RightAtt.Text = (bonus[0] + 2).ToString();
                    else
                        RightAtt.Text = (bonus[0]).ToString();

                    RightDice.Text = $"2*d6 + {bonus[0]}";

                    LeftHand.SelectedIndex = 21;
                    LeftHand.Enabled = OneHanded = false;
                    break;
                    
                case 12:
                    if (weaponSkill[14])
                        RightAtt.Text = (bonus[0] + 2).ToString();
                    else
                        RightAtt.Text = (bonus[0]).ToString();

                    RightDice.Text = $"2*d6 + {bonus[0]}";

                    LeftHand.SelectedIndex = 21;
                    LeftHand.Enabled = OneHanded = false;
                    break;

                case 13:
                    if (weaponSkill[15])
                        RightAtt.Text = (bonus[0] + 2 ).ToString();
                    else
                        RightAtt.Text = (bonus[0]).ToString();

                    RightDice.Text = $"d12 + {bonus[0]}";

                    LeftHand.SelectedIndex = 21;
                    LeftHand.Enabled = OneHanded = false;
                    break;

                case 14:
                    if (weaponSkill[16])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d8 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 15:
                    if (weaponSkill[17])
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2 + DuelPlus).ToString() : (bonus[0] + DuelPlus + 2).ToString();
                    else
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + DuelPlus).ToString() : (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 16:
                    if (weaponSkill[18])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d8 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 17:
                    if (weaponSkill[19])
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2 + DuelPlus).ToString() : (bonus[0] + DuelPlus + 2).ToString();
                    else
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + DuelPlus).ToString() : (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = (bonus[1] >= bonus[0]) ? $"d8 + {bonus[1]}" : $"d8 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 18:
                    if (weaponSkill[20])
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2 + DuelPlus).ToString() : (bonus[0] + DuelPlus + 2).ToString();
                    else
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + DuelPlus).ToString() : (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 19:
                    if (weaponSkill[21])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d6 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 20:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[25])
                            RightAtt.Text = (bonus[0] + DuelPlus + 2).ToString();
                        else
                            RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                        RightDice.Text = $"d4 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[25])
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        RightDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";
                    }
                    OneHanded = true;
                    break;

                case 21:
                    if (weaponSkill[27])
                        RightAtt.Text = (bonus[0] + 2).ToString();
                    else
                        RightAtt.Text = (bonus[0]).ToString();

                    RightDice.Text = $"d10 + {bonus[0]}";

                    LeftHand.SelectedIndex = 21;
                    LeftHand.Enabled = OneHanded = false;
                    break;

                case 22:
                    if (weaponSkill[28])
                        RightAtt.Text = (bonus[0] + 2).ToString();
                    else
                        RightAtt.Text = (bonus[0]).ToString();

                    RightDice.Text = $"d10 + {bonus[0]}";

                    LeftHand.SelectedIndex = 21;
                    LeftHand.Enabled = OneHanded = false;
                    break;

                case 23:
                    if (weaponSkill[29])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d8 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 24:
                    if (weaponSkill[30])
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2 + DuelPlus).ToString() : (bonus[0] + DuelPlus + 2).ToString();
                    else
                        RightAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + DuelPlus).ToString() : (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 25:
                    if(weaponSkill[31])
                        RightAtt.Text = (bonus[0] + 2).ToString();
                    else
                        RightAtt.Text = (bonus[0]).ToString();

                    RightDice.Text = $"d10 + {bonus[0]}";

                    LeftHand.SelectedIndex = 20;
                    LeftHand.Enabled = OneHanded = false;
                    break;

                case 26:
                    if (weaponSkill[32])
                        RightAtt.Text = (bonus[0] + 2 + DuelPlus).ToString();
                    else
                        RightAtt.Text = (bonus[0] + DuelPlus).ToString();

                    RightDice.Text = $"d8 + {bonus[0]}";
                    OneHanded = true;
                    break;

                case 27:
                    RightAtt.Text = (bonus[0] + 2).ToString();
                    RightDice.Text = $"1 + {bonus[0]}";
                    OneHanded = true;
                    break;
            }
        }

        private void LeftHand_SelectedIndexChanged(object sender, EventArgs e)
        {
            armor -= shield;
            shield = 0;

            LeftAtt.Text = LeftDice.Text = "";

            MonchPlus = armorList.SelectedIndex == 0 && classList.SelectedIndex == 5 && LeftHand.SelectedIndex != 16;

            switch (LeftHand.SelectedIndex)
            {
                case 0:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[0])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[0])
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    break;

                case 1:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[1])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[1])
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    break;

                case 2:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[3])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d4 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[3])
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";
                    }
                    break;

                case 3:
                    if (weaponSkill[4])
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();                           
                    else
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? bonus[1].ToString() : bonus[0].ToString();  

                    LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";//f
                    break;

                case 4:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[5])
                            LeftAtt.Text = (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = bonus[0].ToString();

                        LeftDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[5])
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    break;

                case 5:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[6])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d4 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[6])
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";
                    }
                    break;

                case 6:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[7])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[7])
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    break;

                case 7:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[8])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d6 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[8])
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";
                    }
                    break;

                case 8:
                    if (weaponSkill[11])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d8 + {bonus[0]}";
                    break;

                case 9:
                    if (weaponSkill[12])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d8 + {bonus[0]}";
                    break;

                case 10:
                    if (weaponSkill[16])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d8 + {bonus[0]}";
                    break;

                case 11:
                    if (weaponSkill[17])
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? bonus[1].ToString() : bonus[0].ToString();

                    LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";//f
                    break;

                case 12:
                    if (weaponSkill[18])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d8 + {bonus[0]}";
                    break;

                case 13:
                    if (weaponSkill[19])
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? bonus[1].ToString() : bonus[0].ToString();

                    LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d8 + {bonus[1]}" : $"d8 + {bonus[0]}";//f
                    break;

                case 14:
                    if (weaponSkill[20])
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? bonus[1].ToString() : bonus[0].ToString();

                    LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d6 + {bonus[1]}" : $"d6 + {bonus[0]}";//f
                    break;

                case 15:
                    if (weaponSkill[21])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d6 + {bonus[0]}";
                    break;

                case 16:
                    if (!MonchPlus)
                    {
                        if (weaponSkill[25])
                            LeftAtt.Text = (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = bonus[0].ToString();

                        LeftDice.Text = $"d4 + {bonus[0]}";
                    }
                    else
                    {
                        if (weaponSkill[25])
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                        else
                            LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1]).ToString() : (bonus[0]).ToString();

                        LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";
                    }
                    break;

                case 17:
                    if (weaponSkill[29])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d8 + {bonus[0]}";
                    break;

                case 18:
                    if (weaponSkill[30])
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? (bonus[1] + 2).ToString() : (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = (bonus[1] >= bonus[0]) ? bonus[1].ToString() : bonus[0].ToString();

                    LeftDice.Text = (bonus[1] >= bonus[0]) ? $"d4 + {bonus[1]}" : $"d4 + {bonus[0]}";
                    break;

                case 19:
                    if (weaponSkill[32])
                        LeftAtt.Text = (bonus[0] + 2).ToString();
                    else
                        LeftAtt.Text = bonus[0].ToString();

                    LeftDice.Text = $"d8 + {bonus[0]}";
                    break;

                case 20:
                    shield = 2;
                    break;

                case 21:
                    LeftAtt.Text = (bonus[0] + 2).ToString();
                    LeftDice.Text = $"1 + {bonus[0]}";
                    break;
            }

            armor += shield;
            ArmorBox.Text = armor.ToString();
            armorList_SelectedIndexChanged(sender, e);
            RightHand_SelectedIndexChanged(sender, e);
        }

        private void DistanceWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            DistanceAtt.Text = DistDice.Text = "";

            sbyte archer=0;
            if (ChPath.SelectedIndex == 0 && classList.SelectedIndex == 4)
                archer = 2;


            switch (DistanceWeapon.SelectedIndex)
            {
                case 0:
                    if (weaponSkill[9])
                        DistanceAtt.Text = (bonus[1] + 2 + archer).ToString();
                    else
                        DistanceAtt.Text = (bonus[1] + archer).ToString();

                    DistDice.Text = $"d6 + {bonus[1]}";
                    break;

                case 1:
                    if (weaponSkill[10])
                        DistanceAtt.Text = (bonus[1] + 2 + archer).ToString();
                    else
                        DistanceAtt.Text = (bonus[1] + archer).ToString();

                    DistDice.Text = $"d8 + {bonus[1]}";
                    break;

                case 2:
                    if (weaponSkill[22])
                        DistanceAtt.Text = (bonus[1] + 2 + archer).ToString();
                    else
                        DistanceAtt.Text = (bonus[1] + archer).ToString();

                    DistDice.Text = $"d8 + {bonus[1]}";
                    break;

                case 3:
                    if (weaponSkill[23])
                        DistanceAtt.Text = (bonus[1] + 2 + archer).ToString();
                    else
                        DistanceAtt.Text = (bonus[1] + archer).ToString();

                    DistDice.Text = $"d6 + {bonus[1]}";
                    break;

                case 4:
                    if (weaponSkill[24])
                        DistanceAtt.Text = (bonus[1] + 2 + archer).ToString();
                    else
                        DistanceAtt.Text = (bonus[1] + archer).ToString();

                    DistDice.Text = $"d10 + {bonus[1]}";
                    break;

                case 5:
                    if (weaponSkill[26])
                        DistanceAtt.Text = (bonus[1] + 2 + archer).ToString();
                    else
                        DistanceAtt.Text = (bonus[1] + archer).ToString();

                    DistDice.Text = $"d4 + {bonus[1]}";
                    break;

                case 6:
                    if (weaponSkill[33])
                        DistanceAtt.Text = (bonus[1] + 2 + archer).ToString();
                    else
                        DistanceAtt.Text = (bonus[1] + archer).ToString();

                    DistDice.Text = $"1 + {bonus[1]}";
                    break;
            }
        }

        //Обновление данных оружия
        private void WeaponUpdate(object sender, EventArgs e)
        {
            RightHand_SelectedIndexChanged(sender, e);
            LeftHand_SelectedIndexChanged(sender, e);
            DistanceWeapon_SelectedIndexChanged(sender, e);
        }



        //ФЛАЖКИ НАВЫКОВ
        //Умение
        private void AthleticCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 0;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }
                
        private void AcrobaticCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 1;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void HandAgilityCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 2;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void SneakCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 3;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void HistoryCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 4;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void MagicCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 5;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void NatureCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 6;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void DetectivCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 7;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void ReligionCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 8;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void PerceptionCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 9;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void SurviveCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 10;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void TrainingCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 11;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void MedicineCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 12;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void ProniciatsCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 13;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void TerrorCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 14;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void ActingCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 15;
            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        private void LieCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 16;
            more = false;

            more = SkillStatusChange();

            SkillChecking(); 
        }

        private void ConvictionCheck(object sender, EventArgs e)
        {
            IndexOfCheckBox = 17;

            more = false;

            more = SkillStatusChange();

            SkillChecking();
        }

        //проверяющий цикл
        private void SkillChecking()
        {
            foreach (CheckBox box in perks)
            {
                if (box.Checked && !box.Enabled)
                    box.Enabled = false;

                if (box.Checked && box.Enabled)
                    box.Enabled = true;

                if (!box.Checked && ChosenPerks == 0)
                    box.Enabled = false;

                if (!box.Checked && more)
                    box.Enabled = true;
            }

            label21.Text = ChosenPerks.ToString();
        }

        //изменение значения
        private bool SkillStatusChange()
        {
            if (perks[IndexOfCheckBox].Checked)
                ChosenPerks--;
            else
            {
                ChosenPerks++;
                more = true;
            }

            return more;
        }

        //скилы от рас
        private void RaceSkillSave()
        {
            if (RaceBox.SelectedIndex == 12)
            {
                perks[14].Checked = true;
                perks[14].Enabled = false;
            }

            if (RaceBox.SelectedIndex == 3 || RaceBox.SelectedIndex == 2 || RaceBox.SelectedIndex == 4)
            {
                perks[13].Checked = true;
                perks[13].Enabled = false;
            }
        }


        //Мастерство
        private void HistoryMaster(object sender, EventArgs e)
        {
            IndexOfCheckBox = 4;

            bool more = false;

            more = false;

            more = MasterCheckChange();

            OrgiginsSelect(sender, e);
            MasterCheck();
        }

        private void MagicMaster(object sender, EventArgs e)
        {
            IndexOfCheckBox = 5;

            more = false;

            more = MasterCheckChange();

            OrgiginsSelect(sender, e);
            MasterCheck();
        }

        private void NatureMaster(object sender, EventArgs e)
        {
            IndexOfCheckBox = 6;

            more = false;

            more = MasterCheckChange();

            OrgiginsSelect(sender, e);
            MasterCheck();
        }

        private void ReligionMaster(object sender, EventArgs e)
        {
            IndexOfCheckBox = 8;

            more = false;

            more = MasterCheckChange();

            OrgiginsSelect(sender, e);
            MasterCheck();
        }
        
        private void SurvivalMaster(object sender, EventArgs e)
        {
            IndexOfCheckBox = 10;

            more = false;

            more = MasterCheckChange();

            OrgiginsSelect(sender, e);
            MasterCheck();
        }

        private void TrainingMaster(object sender, EventArgs e)
        {
            IndexOfCheckBox = 11;

            more = false;

            more = MasterCheckChange();

            OrgiginsSelect(sender, e);
            MasterCheck();
        }

        

        //проверяющий цикл
        private void MasterCheck()
        {
            foreach (CheckBox box in perksMaster)
            {
                if (box.Checked && !box.Enabled)
                    box.Enabled = false;

                if (box.Checked && box.Enabled)
                    box.Enabled = true;

                if (!box.Checked && ChosenMaster == 0)
                    box.Enabled = false;

                if (!box.Checked && ChosenMaster > 0)
                    box.Enabled = true;
            }
        }

        //Ищменение значений
        private bool MasterCheckChange()
        {
            if (perksMaster[IndexOfCheckBox].Checked)
            {
                ChosenMaster--;
                perks[IndexOfCheckBox].Checked = true;
                perks[IndexOfCheckBox].Enabled = false;
            }
            else
            {
                ChosenMaster++;
                more = true;
                perks[IndexOfCheckBox].Checked = false;
                perks[IndexOfCheckBox].Enabled = true;
            }

            return more;
        }

        //СИСТЕМНЫЕ МЕТОДЫ
        //сохранение перса
        private void Save_Click(object sender, EventArgs e)
        {
            name = CharacterName.Text + " " + CharacterSurname.Text;
            while (File.Exists($"C:\\D&D Characters\\Персонаж {name} {Num}.txt"))
            {
                Num++;
            }

            bool chooseAll = true;
            string choose="";
            foreach (CheckBox box in boxes)
            {
                if(box.Checked)
                    choose += box.Text;

                if (chooseAll)
                    chooseAll = box.Checked;
            }

            if (point == 0 && CharacterName.Text != "" && label17.Text != "Мировозрение: " && (ChosenPerks==0|| chooseAll))
            {
                File.AppendAllLines($"C:\\D&D Characters\\Персонаж {name} {Num}.txt", new string[]
                    {
                        "Имя: "+name, label9.Text, label11.Text, label17.Text, Environment.NewLine, $"Здоровье: {hp}", $"Класс брони: {armor}({armorList.SelectedItem})", $"Скорость: {speed}", $"Инициатива: {IniciativaBox.Text}",
                        Environment.NewLine,"Сила: " +stat[0].ToString()+"\tМодификатор: "+bonus[0],"Ловкость: "+stat[1].ToString()+"\tМодификатор: "+bonus[1],"Выносливость: "+stat[2].ToString()+"\tМодификатор: "+bonus[2],
                        "Интеллект: "+stat[3].ToString()+"\tМодификатор: "+bonus[3],"Мудрость: "+stat[4].ToString()+"\tМодификатор: "+bonus[4],"Харизма: "+stat[5].ToString()+"\tМодификатор: "+bonus[5],Environment.NewLine, "Умел в: "+ choose
                        ,Environment.NewLine,"Снаряжение: "+startInventory+Environment.NewLine+ armorList.SelectedItem+" "+RightHand.SelectedItem+" "+LeftHand.SelectedItem+" "+DistanceWeapon.SelectedItem
                    });
                MessageBox.Show($"Персонаж {name} сохранен", "Информация о сохранении", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Потратьте все очки характеристик, выберите мировозрение персонажа и выберите навыки в которых персонаж умелый для сохранения", "Информация о сохранении", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            Num = 0;

            try
            {
                foreach (Process process in Process.GetProcessesByName("Notepad"))
                {
                    process.Kill();
                }
                File.Delete("C:\\D&D Characters\\Расовый Костыль.txt");
                File.Delete("C:\\D&D Characters\\Классовый Костыль.txt");
                File.Delete("C:\\D&D Characters\\Костыль.txt");
            }
            catch
            {
            }
        }
        

        //ВЫВОД ИНФОРМАЦИИ
        private void RaceBook_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Process process in Process.GetProcessesByName("Notepad"))
                {
                    process.Kill();
                }
            }
            catch
            {
            }

            string fileName = "C:\\D&D Characters\\Расовый Костыль.txt";
            ProcessStart(fileName);
        }

        private void ClassBook_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Process process in Process.GetProcessesByName("Notepad"))
                {
                    process.Kill();
                }
            }
            catch
            {
            }

            string fileName = "C:\\D&D Characters\\Классовый Костыль.txt";
            ProcessStart(fileName);
        }

        private void OriginsBook_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Process process in Process.GetProcessesByName("Notepad"))
                {
                    process.Kill();
                }
            }
            catch
            {
            }

            string fileName = "C:\\D&D Characters\\Костыль.txt";
            ProcessStart(fileName);
        }

        //Запуск блокнота с информацией
        private void ProcessStart(string fileName)
        {
            File.Delete(fileName);

            StreamWriter writer = new StreamWriter(File.OpenWrite(fileName));
            writer.WriteLine(RaceSpec);

            File.SetAttributes(fileName, FileAttributes.ReadOnly);
            File.SetAttributes(fileName, FileAttributes.Hidden);
            Process.Start(fileName);

            writer.Close();
        }
    }
}