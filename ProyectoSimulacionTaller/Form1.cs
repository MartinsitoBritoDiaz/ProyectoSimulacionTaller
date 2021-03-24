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

        bool pasoC1 = false;
        bool pasoC2 = false;
        bool pasoC3 = false;
        bool pasoC4 = false;
        bool pasoC5 = false;

        Queue ColaLlegada = new Queue();

        List<int> ListaAlmacen = new List<int>();

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
                    CantMarcos4Taller += aux;
                    Marcos4Label.Text = Convert.ToString(CantMarcos4Taller);
                }
                else
                {
                    if ((llegada > 60) && (llegada <= 100))
                    {
                        aux = 6;
                        CantMarcos6Taller += aux;
                        Marcos6Label.Text = Convert.ToString(CantMarcos6Taller);
                    }
                }


                label9.Text = Convert.ToString(CantMarcos4Taller + CantMarcos6Taller);
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
            }
            
            if(Carpintero2() != -1)
            {
                ListaAlmacen.Add(Carpintero2());
            }
            
            if(Carpintero3() != -1)
            {
                ListaAlmacen.Add(Carpintero3());
            }
            
            if(Carpintero4() != -1)
            {
                ListaAlmacen.Add(Carpintero4());
            }
            
            if(Carpintero5() != -1)
            {
                ListaAlmacen.Add(Carpintero5());
            }

            label4.Text = Convert.ToString(ListaAlmacen.Count());
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


                label5.Text = Convert.ToString((int)ColaLlegada.Count);

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
        
        public void Almnacen()
        {

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
    }
}
