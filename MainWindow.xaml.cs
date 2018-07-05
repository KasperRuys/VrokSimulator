using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VrokFightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int SurpriseCounter = 0;
        bool Surpriseround = false;
        int VrokDCCheck;
        decimal RoundAverage;
        int[] VrokAttack = new int[] { 0, 0 };
        decimal VrokAverageDamage;
        decimal VrokTurnCounter;
        int VrokAC = 17;
        decimal VrokRoundDamage = 0;
        int Vrokmaxhp = 119;
        bool VrokReckless = false;
        decimal simaantal;
        int vrokinitiative;
        bool vrokrage = false;
        int Vrokattack1;
        int Vrokattack2;
        int Vrokhpend;
        int Vrokwin;
        decimal VrokwinPercentage;
        decimal Vrokhpaverage;

        int AmountOfDie;
        int NumberSidedDie;
        int FlatDamage;

        decimal EnemyhpAverage;
        int shadowjumphits;
        decimal shadowjumpdamage;
        int shadowjumpused;
        int ShadowJumpamount;
        int Boneclawin;
        decimal BoneclawtotalDamagedone;
        int Boneclawhpend;
        int[] BoneclawAttack = new int[] { 0, 0 };
        int Boneclawmaxhp = 127;
        int Boneclawattack1;
        int Boneclawattack2;
        int boneclawinitiative;
        decimal BoneclawRoundDamage = 0;
        int BoneClawAC = 13;
        Random RollDice = new Random();

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VrokReckless = false;
                vrokrage = false;
                ShadowJumpamount = Convert.ToInt16(txtShadowJumpAmount.Text);
                simaantal = Convert.ToInt32(txtsims.Text.Replace(" ", "").Replace(".", "").Replace(",", ""));

                AmountOfDie = Convert.ToInt32(txtAmountofdamagedie.Text);
                NumberSidedDie = Convert.ToInt32(txtSideddie.Text) ;
                FlatDamage = Convert.ToInt32(txtflatdamageplus.Text);

                VrokTurnCounter = 0;
                int simteller = 0;
                Vrokmaxhp = 119;
                Boneclawmaxhp = 127;


                do
                {
                    // MessageBox.Show(Convert.ToString(SurpriseCounter));
                  /*  if (Surpriseround == true && SurpriseCounter < 1)
                    {
                        SurpriseCounter++;
                        Boneclawattack1 += RollDice.Next(1, 20) + 8;
                        BoneclawAttack[0] = Boneclawattack1;
                        BoneclawRoundDamage += 4;
                        for (int i = 0; i < 3; i++)
                        {
                            BoneclawRoundDamage += RollDice.Next(1, 10);
                        }
                        

                    }*/
                    Initiative();
                    #region Vrokgoes1st
                    if (vrokinitiative > boneclawinitiative || vrokinitiative == boneclawinitiative)
                    {
                        do
                        {
                            #region VroksRound
                            if (Vrokmaxhp > 0 && Boneclawmaxhp > 0)
                            {
                                VrokTurnCounter++;
                                #region CheckReckless&Rage
                                VrokCheckTraits();
                                #endregion

                                #region RollToHitVrok
                                VrokToHit();
                                #endregion

                                // 2 ATTACKS IN A ROUND

                                #region RollForDamageVrok
                                VrokRollDamage();
                                #endregion
                            }
                            #endregion


                            //BONECLAWS TURN <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                            #region EnemyRound
                            if (Vrokmaxhp > 0 && Boneclawmaxhp > 0)
                            {


                                if (chbShadowjump.IsChecked == true && ShadowJumpamount > shadowjumpused)
                                {
                                    #region Shadowjump
                                    ShadowJump();
                                    #endregion
                                }


                                else
                                {
                                    #region ClawAttack
                                    #region RollToHitEnemy
                                    EnemyTohit();
                                    #endregion

                                    // 2 ATTACKS IN A ROUND

                                    #region RollForDamageEnemy
                                    EnemyRollDamage();
                                    #endregion
                                    #endregion
                                }

                            }
                            #endregion

                        } while (Boneclawmaxhp > 0 && Vrokmaxhp > 0);

                        WinrateAndReset();
                        simteller++;
                    }
                    #endregion

                    #region Enemygoesfirst
                    if (vrokinitiative < boneclawinitiative)
                    {
                        do
                        {
                            //BONECLAWS TURN <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                            if (Vrokmaxhp > 0 && Boneclawmaxhp > 0)
                            {
                                VrokTurnCounter++;

                                if (chbShadowjump.IsChecked == true && ShadowJumpamount > shadowjumpused)
                                {
                                    #region Shadowjump
                                    ShadowJump();
                                    #endregion
                                }

                                else
                                {
                                    #region RolltohitEnemy
                                    EnemyTohit();
                                    #endregion

                                    #region RollForDamageEnemy
                                    EnemyRollDamage();
                                    #endregion
                                }

                                // 2 ATTACKS IN A ROUND

                            }


                            // VROKS TURN

                            if (Vrokmaxhp > 0 && Boneclawmaxhp > 0)
                            {
                                #region CheckReckless&Rage
                                VrokCheckTraits();

                                #endregion

                                #region RolltohitVrok
                                VrokToHit();

                                #endregion

                                // 2 ATTACKS IN A ROUND
                                #region RollForDamageVrok
                                VrokRollDamage();
                                #endregion

                            }


                        } while (Boneclawmaxhp > 0 && Vrokmaxhp > 0);
                        WinrateAndReset();
                        simteller++;
                    }

                    #endregion

                } while (simaantal > simteller);


                #region LABELS
                lblaveragedamagevrok.Content = Convert.ToString(Math.Round((VrokAverageDamage / VrokTurnCounter), 2));
                lblavgdamagebc.Content = Convert.ToString(Math.Round((BoneclawtotalDamagedone / VrokTurnCounter), 2));
                /*         lbltotaldamagevrok.Content = Convert.ToString(VrokAverageDamage);
                         lblbcinitiative.Content = Convert.ToString(boneclawinitiative);
                         lbltotaldamageboneclaw.Content = Convert.ToString(BoneclawtotalDamagedone) ;
                         lblinitiativeVrok.Content = Convert.ToString(vrokinitiative);
                         lblhpbc.Content = Convert.ToString(Boneclawhpend);
                         lblhpvrok.Content = Convert.ToString(Vrokhpend);*/
                lblVrokhpaverage.Content = Math.Round((((Convert.ToDecimal(Vrokhpaverage)) / Convert.ToDecimal(simaantal))), 2);
                lblBoneclawhpaverage.Content = Math.Round((((Convert.ToDecimal(EnemyhpAverage)) / Convert.ToDecimal(simaantal))), 2);

                lblboneclawwins.Content = Convert.ToString(Boneclawin);
                lblvrokwins.Content = Convert.ToString(Vrokwin);
                lblshadowjumphits.Content = "Average Hits: " + (Math.Round((((Convert.ToDecimal(shadowjumphits)) / Convert.ToDecimal(simaantal))), 2));
                VrokwinPercentage = Math.Round((((Convert.ToDecimal(Vrokwin)) / (Convert.ToDecimal(simaantal))) * 100), 2);
                lblVrokwinrate.Content = Convert.ToString(VrokwinPercentage + "%");
                RoundAverage = Math.Round((((Convert.ToDecimal(VrokTurnCounter)) / Convert.ToDecimal(simaantal))), 2);
                lblAveragerounds.Content = Convert.ToString(RoundAverage);
                #endregion

                #region ResetAveragecounters
                shadowjumphits = 0;
                VrokAverageDamage = 0;
                BoneclawtotalDamagedone = 0;
                VrokTurnCounter = 1;
                Vrokwin = 0;
                Boneclawin = 0;
                Vrokhpaverage = 0;
                EnemyhpAverage = 0;
                #endregion
            }
            catch (Exception)
            {

                MessageBox.Show("Please fill in how many iterations.", "No Simulation amount", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Tuple<int, int> Initiative()
        {
            boneclawinitiative = (RollDice.Next(1, 20)) + 3;
            int vrokinitiative1 = RollDice.Next(1, 20) + 2;
            int vrokinitiative2 = RollDice.Next(1, 20) + 2;
            if (vrokinitiative1 > vrokinitiative2)
            {
                vrokinitiative = vrokinitiative1;
            }
            else
            {
                vrokinitiative = vrokinitiative2;
            }
            return Tuple.Create(boneclawinitiative, vrokinitiative);
        }

        public void VrokCheckTraits()
        {
            if (chbReckless.IsChecked == true)
            {
                VrokReckless = true;
            }
            else
            {
                VrokReckless = false;
            }
            if (chbRage.IsChecked == true)
            {
                vrokrage = true;
            }
            else
            {
                vrokrage = false;
            }
        }

        public void VrokToHit()
        {
            
            for (int i = 0; i < 2; i++)
            {
                if (VrokReckless == true)
                {


                    Vrokattack1 += RollDice.Next(1, 20) + 11;
                    Vrokattack2 += RollDice.Next(1, 20) + 11;
                    if (Vrokattack1 > Vrokattack2)
                    {
                        VrokAttack[i] = Vrokattack1;
                    }
                    else
                    {
                        VrokAttack[i] = Vrokattack2;
                    }
                    // MessageBox.Show(Convert.ToString(Boneclawmaxhp + ", attack1: " + Vrokattack1 + ", attack2:" + Vrokattack2));
                    Vrokattack1 = 0;
                    Vrokattack2 = 0;
                }
                else
                {
                    Vrokattack1 += RollDice.Next(1, 20) + 11;
                    VrokAttack[i] = Vrokattack1;
                }
                Vrokattack1 = 0;
            }
        }

        public void VrokRollDamage()
        {
            for (int f = 0; f < 2; f++)
            {
                if (VrokAttack[f] >= BoneClawAC)
                {
                    VrokRoundDamage += RollDice.Next(1, 12) + 10;
                    //IF HE CRITS
                    if (VrokAttack[f] == 31)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            VrokRoundDamage += RollDice.Next(1, 12);
                        }
                    }
                }

            }
            Boneclawmaxhp -= Convert.ToInt32(VrokRoundDamage);
            VrokAverageDamage += VrokRoundDamage;
            VrokRoundDamage = 0;
        }

        public void ShadowJump()
        {
            shadowjumpused++;
            VrokDCCheck = RollDice.Next(1, 20) + 9;
            if (VrokDCCheck < 14)
            {
                shadowjumphits++;
                shadowjumpdamage += 2;
                for (int i = 0; i < 5; i++)
                {
                    shadowjumpdamage += RollDice.Next(1, 12);
                }

                if (vrokrage == false)
                {
                    Vrokmaxhp -= Convert.ToInt16(shadowjumpdamage);
                }
                else
                {
                    Vrokmaxhp -= Convert.ToInt16(Math.Round((shadowjumpdamage / 2), 2));
                }
            }
        }

        public void EnemyTohit()
        {
            
            if (VrokReckless == true)
            {
                for (int i = 0; i < 2; i++)
                {
                    Boneclawattack1 += RollDice.Next(1, 20) + 8;
                    Boneclawattack2 += RollDice.Next(1, 20) + 8;
                    if (Boneclawattack1 > Boneclawattack2)
                    {
                        BoneclawAttack[i] = Boneclawattack1;
                    }
                    else
                    {
                        BoneclawAttack[i] = Boneclawattack2;
                    }
                    // MessageBox.Show(Convert.ToString(Boneclawmaxhp + ", attack1: " + Vrokattack1 + ", attack2:" + Vrokattack2));
                    Boneclawattack1 = 0;
                    Boneclawattack2 = 0;

                }

            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    Boneclawattack1 += RollDice.Next(1, 20) + 8;
                    BoneclawAttack[i] = Boneclawattack1;
                    // MessageBox.Show(Convert.ToString(Boneclawmaxhp + ", attack1: " + Vrokattack1 + ", attack2:" + Vrokattack2));
                    Boneclawattack1 = 0;
                }
            }
        }
        public void EnemyRollDamage()
        {
            for (int f = 0; f < 2; f++)
            {
                if (BoneclawAttack[f] >= VrokAC)
                {
                    BoneclawRoundDamage += 4;
                    for (int i = 0; i < 3; i++)
                    {
                        BoneclawRoundDamage += RollDice.Next(1, 10);
                    }

                    //IF HE CRITS
                    if (BoneclawAttack[f] == 28)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            BoneclawRoundDamage += RollDice.Next(1, 10);
                        }
                    }

                }

            }
            if (vrokrage == true)
            {
                Vrokmaxhp -= Convert.ToInt32(BoneclawRoundDamage / 2);
                BoneclawtotalDamagedone += (BoneclawRoundDamage / 2);
            }
            else
            {
                Vrokmaxhp -= Convert.ToInt32(BoneclawRoundDamage);
                BoneclawtotalDamagedone += (BoneclawRoundDamage);
            }
            BoneclawRoundDamage = 0;
        }

        public void WinrateAndReset()
        {
            if (Vrokmaxhp <= 0)
            {
                Vrokmaxhp = 0;
                Boneclawin++;
            }
            if (Boneclawmaxhp <= 0)
            {
                Boneclawmaxhp = 0;
                Vrokwin++;
            }
            Vrokhpaverage += Vrokmaxhp;
            EnemyhpAverage += Boneclawmaxhp;
            shadowjumpdamage = 0;
            shadowjumpused = 0;
            Vrokhpend = Vrokmaxhp;
            Boneclawhpend = Boneclawmaxhp;
            Boneclawmaxhp = 127;
            Vrokmaxhp = 119;
            SurpriseCounter = 0;
        }
        private void chbShadowjump_Checked_1(object sender, RoutedEventArgs e)
        {
            txtShadowJumpAmount.Visibility = System.Windows.Visibility.Visible;
            lblshadowjumphits.Visibility = System.Windows.Visibility.Visible;
        }

        private void chbShadowjump_Unchecked(object sender, RoutedEventArgs e)
        {
            txtShadowJumpAmount.Visibility = System.Windows.Visibility.Hidden;
            lblshadowjumphits.Visibility = System.Windows.Visibility.Hidden;
        }
/*
        private void chbSurprise_Checked(object sender, RoutedEventArgs e)
        {
            Surpriseround = true;
        }
        

        private void chbSurprise_Unchecked_1(object sender, RoutedEventArgs e)
        {
            Surpriseround = false;
        }*/
    }








}
