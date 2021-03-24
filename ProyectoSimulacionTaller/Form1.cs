using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSimulacionTaller
{
    public partial class Form1 : Form
    {
        public int Horas = 0;
        public int Minutos = 0;
        public int Segundos = 0;

        public int CantMarcos4Taller = 0;
        public int CantMarcos6Taller = 0;
        
        
        public int Contador = 0;

        public int HoraActualC1 = 0;
        public int HoraActualC2 = 0;
        public int HoraActualC3 = 0;
        public int HoraActualC4 = 0;
        public int HoraActualC5 = 0;

        public int total =0;

        bool pasoC1 = false;
        bool pasoC2 = false;
        bool pasoC3 = false;
        bool pasoC4 = false;
        bool pasoC5 = false;

        Queue ColaLlegada = new Queue();

        List<int> ListaAlmacen = new List<int>();
        List<int> ListaHoraEntradaAlmacen = new List<int>();

        List<int> ListaPintado = new List<int>();
        Queue ColaEmpaque = new Queue();

        int TiempoMantenimiento = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public double Probabilidad()
        {
            Random auxiliar = new Random();
            double probabilidad;

            probabilidad = auxiliar.NextDouble();

            probabilidad *= 100;

            return probabilidad;
        }

        public void Llegada()
        {
            Minutos++; 

            double llegada = Probabilidad();
            
            if(Minutos == 60)
            {
                Horas++;

                if(Horas == 24)
                {
                    Horas = 00;
                    Minutos = 00;
                }


                Minutos = 00;
            }

            if((Horas % 5 == 0) && (Minutos == 1))
            {
                int aux = 0;

                if ((llegada >= 0) && (llegada <= 60))
                {
                    aux = 4;
                    CantMarcos4Taller ++;
                    
                    Marcos4Label.Text = Convert.ToString(CantMarcos4Taller);
                }
                else
                {
                    if ((llegada > 60) && (llegada <= 100))
                    {
                        aux = 6;
                        CantMarcos6Taller ++;
                        Marcos6Label.Text = Convert.ToString(CantMarcos6Taller);
                    }
                }
                total += aux;


                //Totallabel.Text = Convert.ToString(CantMarcos4Taller*(4) + CantMarcos6Taller*(6));
                Totallabel.Text = Convert.ToString(aux);
                AsignarMarcosLleganAEnsamble(aux);
            }


            Ensambalar();

            if (Minutos < 10)
                Minutoslabel.Text = Convert.ToString("0" + Minutos);
            else
                Minutoslabel.Text = Convert.ToString(Minutos);


            if (Horas < 10)
                Horaslabel.Text = Convert.ToString("0 " + Horas);
            else
                Horaslabel.Text = Convert.ToString(Horas);

            Almacen();
            ProcesoPintura();
            Empaque();
            
        }

    
        

        public void AsignarMarcosLleganAEnsamble(int aux)
        {
            //Asignacion de marcos Desmontados
            if (aux == 4)
            {
                ColaLlegada.Enqueue(Contador);

                Contador++;

                ColaLlegada.Enqueue(Contador);

                Contador++;

                ColaLlegada.Enqueue(Contador);

                Contador++;

                ColaLlegada.Enqueue(Contador);
            }
            else
            {
                if (aux == 6)
                {
                    ColaLlegada.Enqueue(Contador);

                    Contador++;

                    ColaLlegada.Enqueue(Contador);

                    Contador++;

                    ColaLlegada.Enqueue(Contador);

                    Contador++;

                    ColaLlegada.Enqueue(Contador);

                    Contador++;

                    ColaLlegada.Enqueue(Contador);

                    Contador++;

                    ColaLlegada.Enqueue(Contador);
                }
            }
        }

        public void Ensambalar()
        {

            if(Carpintero1() != -1)
            {
                ListaAlmacen.Add(Carpintero1());
                ListaHoraEntradaAlmacen.Add(Horas);
            }
            
            if(Carpintero2() != -1)
            {
                ListaAlmacen.Add(Carpintero2());
                ListaHoraEntradaAlmacen.Add(Horas);
            }
            
            if(Carpintero3() != -1)
            {
                ListaAlmacen.Add(Carpintero3());
                ListaHoraEntradaAlmacen.Add(Horas);
            }
            
            if(Carpintero4() != -1)
            {
                ListaAlmacen.Add(Carpintero4());
                ListaHoraEntradaAlmacen.Add(Horas);
            }
            
            if(Carpintero5() != -1)
            {
                ListaAlmacen.Add(Carpintero5());
                ListaHoraEntradaAlmacen.Add(Horas);
            }

            Almacenlabel.Text = Convert.ToString(ListaAlmacen.Count());
        }

        public int Carpintero1()
        {

            if(ColaLlegada.Count > 0)
            {

                if (pasoC1)
                {
                    HoraActualC1 = Horas;
                    pasoC1 = false;
                    return (int)ColaLlegada.Dequeue();
                }

                if (!pasoC1)
                {
                    if ((Horas > HoraActualC1) && (HoraActualC1 + 2 == Horas))
                        pasoC1 = true;

                    if (HoraActualC1 == 22)
                        if (Horas == 0)
                            pasoC1 = true;

                    if (HoraActualC1 == 23)
                        if (Horas == 1)
                            pasoC1 = true;

                }


                Ensamblajelabe.Text = Convert.ToString((int)ColaLlegada.Count);

            }

            return -1;

        }
        public int Carpintero2()
        {
            if (ColaLlegada.Count > 0)
            {
                if (pasoC2)
                {
                    HoraActualC2 = Horas;
                    pasoC2 = false;
                    return (int)ColaLlegada.Dequeue();
                }

                if (!pasoC2)
                {
                    if ((Horas > HoraActualC2) && (HoraActualC2 + 3 == Horas))
                        pasoC2 = true;

                    if (HoraActualC2 == 21)
                        if (Horas == 0)
                            pasoC2 = true;

                    if (HoraActualC2 == 22)
                        if (Horas == 1)
                            pasoC2 = true;
                }
            }
            return -1;
        }
        public int Carpintero3()
        {
            if (ColaLlegada.Count > 0)
            {
                if (pasoC3)
                {
                    HoraActualC3 = Horas;
                    pasoC3 = false;
                    return (int)ColaLlegada.Dequeue();
                }

                if (!pasoC3)
                {
                    if ((Horas > HoraActualC3) && (HoraActualC3 + 4 == Horas))
                        pasoC3 = true;

                    if (HoraActualC3 == 21)
                        if (Horas == 0)
                            pasoC3 = true;

                    if (HoraActualC3 == 22)
                        if (Horas == 1)
                            pasoC3 = true;
                }
            }
            return -1;
        }
        public int Carpintero4()
        {
            if (ColaLlegada.Count > 0)
            {

                if (pasoC4)
                {
                    HoraActualC4 = Horas;
                    pasoC4 = false;
                    return (int)ColaLlegada.Dequeue();
                }

                if (!pasoC4)
                {
                    if ((Horas > HoraActualC4) && (HoraActualC4 + 5 == Horas))
                        pasoC4 = true;

                    if (HoraActualC4 == 20)
                        if (Horas == 0)
                            pasoC4 = true;

                    if (HoraActualC4 == 21)
                        if (Horas == 1)
                            pasoC4 = true;
                }
            }
            return -1;
        }
        public int Carpintero5()
        {
            if (ColaLlegada.Count > 0)
            {

                if (pasoC5)
                {
                    HoraActualC5 = Horas;
                    pasoC5 = false;
                    return (int)ColaLlegada.Dequeue();
                }

                if (!pasoC5)
                {
                    if ((Horas > HoraActualC5) && (HoraActualC5 + 6 == Horas))
                        pasoC5 = true;

                    if (HoraActualC5 == 19)
                        if (Horas == 0)
                            pasoC5 = true;

                    if (HoraActualC5 == 20)
                        if (Horas == 1)
                            pasoC5 = true;
                }
            }
            return -1;
        }


        public void Almacen()
        {

            if (ListaAlmacen.Count > 1)
            {
                /* foreach (var horaLlegada in ListaEntradaAlmacen)
                  {
                      if (Horas == horaLlegada)
                      {
                          ListaPintado.Add(1);
                          aux = ListaEntradaAlmacen.IndexOf(horaLlegada);

                      }

                  }*/

                if (ListaHoraEntradaAlmacen.First() == Horas)
                {
                    ListaPintado.Add(1);
                    ListaHoraEntradaAlmacen.Remove(Horas);
                    ListaAlmacen.RemoveAt(0);

                }
            }

            Pinturalabel.Text = ListaPintado.Count().ToString();

        }

        int TiempoPintando = 0;
        int aux2Pintura = 0; //auxiliar para identificar si va a se 10, 11, 12 ... 20 minutos
        public void ProcesoPintura()
        {
            if((total%20)!=0)
            if (ListaPintado.Count > 0)
            {
                TiempoPintando++; //Tiempo que va transcurriendo pintando 1

                if (TiempoPintando == (aux2Pintura+10)) //si el tiempo alcanzo lo requerido
                {
                    TiempoPintando = 0;
                    

                    if (aux2Pintura == 10)
                        aux2Pintura = 0;

                    double inspeccion = Probabilidad();

                    if(inspeccion>=0 && inspeccion < 90)
                    {
                        ColaEmpaque.Enqueue(1);
                        ListaPintado.RemoveAt(0);
                        aux2Pintura++; //para saber si va en el 1, 2....
                    }
                    else
                        if(inspeccion>90 && inspeccion <= 100)
                        {
                            timer1.Stop();
                            MessageBox.Show("Este marco no ha superado con éxito la inspeccion, debe ser retrabajado", "Aviso!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            timer1.Start();
                            ListaPintado.RemoveAt(0);
                            ListaPintado.Add(1);
                            aux2Pintura++; //para saber si va en el 1, 2....
                        }

                }
            }
            

            Empaquelabel.Text = Convert.ToString((int)ColaEmpaque.Count);
        }

        int Despachado = 0;
        int TiempoEmpacando = 0;
        int AuxEmpaque = 0;
        public void Empaque()
        {
            if (ColaEmpaque.Count > 0)
            {
                TiempoEmpacando++;

                if (TiempoEmpacando == (AuxEmpaque + 10))
                {
                    if (AuxEmpaque == 5)
                        AuxEmpaque = 0;

                    TiempoEmpacando = 0;

                    Despachado++;
                    ColaEmpaque.Dequeue();
                }
            }

            Despachadoslabel.Text = Despachado.ToString();
        }

        public bool Mantenimiento()
        {
            double aux = Probabilidad();
            bool EnMantenimiento = false;
            if ((total % 20)==0)
            {
                if(aux>=0 && aux < (1 / 3 *100))
                {
                    TiempoMantenimiento = 30;
                    EnMantenimiento = true;
                }else
                    if(aux>(1/3*100) && aux < (1 / 2 * 100)) 
                    { 
                        TiempoMantenimiento=45;
                        EnMantenimiento = true;
                    }
                    else
                        if(aux>(1/2*100) && aux <= (100)) 
                        {
                            TiempoMantenimiento = 60;
                            EnMantenimiento = true;
                        }
                            
            }

            return EnMantenimiento;
        }

        public void Ejecutar()
        {
            Llegada();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Ejecutar();
        }

        private void Minutoslabel_Click(object sender, EventArgs e)
        {

        }

        private void Marcos4Label_Click(object sender, EventArgs e)
        {

        }
    }
}
