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

        bool ControlAlmacen;

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
            this.WindowState = FormWindowState.Maximized;
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


            //Aqui se quita la imagen de los marcos desarmados
            //Lo coloque en 50 minutos para que se vean un poco las cajas
            if(Minutos == 30)
            {

                Marcos6PictureBox.Visible = false;
                Marcos4PictureBox.Visible = false;

                Caja1PictureBox.Visible = false;
                Caja2pictureBox.Visible = false;
                Caja3pictureBox.Visible = false;
                Caja4pictureBox.Visible = false;
                Caja5pictureBox.Visible = false;

                CaminonPictureBox.Visible = true;

                if (CaminonPictureBox.Left < 40)
                {
                    CaminonPictureBox.Left += 10;
                }

            }


            //Para mostrar las cajas que llegan
            if (ColaLlegada.Count > 0)
                ColaEntradapictureBox.Visible = true;
            else
                ColaEntradapictureBox.Visible = false;


            if ((Horas % 5 == 0) && (Minutos == 1))
            {
                

                int aux = 0;

                if ((llegada >= 0) && (llegada <= 60))
                {
                    aux = 4;
                    CantMarcos4Taller ++;
                    Marcos4PictureBox.Visible = true;

                    CaminonPictureBox.Left = -20;

                    Marcos4Label.Text = Convert.ToString(CantMarcos4Taller);
                }
                else
                {
                    if ((llegada > 60) && (llegada <= 100))
                    {
                        aux = 6;
                        CantMarcos6Taller ++;
                        Marcos6PictureBox.Visible = true;

                        CaminonPictureBox.Left = -20;

                        Marcos6Label.Text = Convert.ToString(CantMarcos6Taller);
                    }
                }
                total += aux;

                //Totallabel.Text = Convert.ToString(CantMarcos4Taller*(4) + CantMarcos6Taller*(6));
                Totallabel.Text = Convert.ToString(total);
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


            if(ListaAlmacen.Count() > 0)
            {
                CajasAlmacenPictureBox.Visible = true;
            }

        }

    
        

        public void AsignarMarcosLleganAEnsamble(int aux)
        {
            //Asignacion de marcos Desmontados
            if (aux == 4)
            {

                Contador++;

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
                    Caja1PictureBox.Visible = true;

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
                    Caja2pictureBox.Visible = true;
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
                    Caja3pictureBox.Visible = true;
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
                    Caja4pictureBox.Visible = true;
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

            if (ColaLlegada.Count > 5)
            {

                if (pasoC5)
                {
                    HoraActualC5 = Horas;
                    pasoC5 = false;
                    Caja3pictureBox.Visible = true;

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
                    MApagadapictureBox.Visible = false;
                    MEncendidapictureBox.Visible = true;
                    
                    ListaPintado.Add(1);
                    ListaHoraEntradaAlmacen.Remove(Horas);
                    ListaAlmacen.RemoveAt(0);

                }
            }

            Pinturalabel.Text = ListaPintado.Count().ToString();

        }

        int TiempoPintando = 0;
        int aux2Pintura = 0; //auxiliar para identificar si va a se 10, 11, 12 ... 20 minutos
        bool mensaje = false;
        double aux;
        int contadorPintados=0;
        int TiempoTranscurridoMantenimiento = 0;
        public void ProcesoPintura()
        {
           if (contadorPintados!=20)
            {
                mensaje = false;
                TiempoMantenimiento = 0;

                if (ListaPintado.Count > 0)
                {
                    TiempoPintando++; //Tiempo que va transcurriendo pintando 1

                    if (TiempoPintando == (aux2Pintura + 10)) //si el tiempo alcanzo lo requerido
                    {
                        TiempoPintando = 0;


                        if (aux2Pintura == 10)
                            aux2Pintura = 0;

                        double inspeccion = Probabilidad();

                        if (inspeccion >= 0 && inspeccion < 90)
                        {
                            MApagadapictureBox.Visible = true;
                            MEncendidapictureBox.Visible = false;

                            ColaEmpaque.Enqueue(1);
                            ListaPintado.RemoveAt(0);
                            aux2Pintura++; //para saber si va en el 1, 2....
                            contadorPintados++;
                        }
                        else
                            if (inspeccion > 90 && inspeccion <= 100)
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
            }
            else
            {
               
                aux = Probabilidad();
                
                if (mensaje==false) //Si no ha mostrado el mensaje
                {
                    timer1.Stop();
                    if (aux >= 0 && aux < 33)
                    {
                        TiempoMantenimiento = 30;
                        MessageBox.Show("Mantenimiento 30 Minutos");
                    }
                    else
                    if (aux > (33) && aux < (66))
                    {
                        TiempoMantenimiento = 45;
                        MessageBox.Show("Mantenimiento 45 Minutos");
                    }
                    else
                        if (aux > (66) && aux <= (100))
                    {
                        TiempoMantenimiento = 60;
                        MessageBox.Show("Mantenimiento 60 Minutos");
                    }
                    mensaje = true;
                    timer1.Start();

                }


                TiempoTranscurridoMantenimiento++;
                if (TiempoTranscurridoMantenimiento == TiempoMantenimiento)
                {
                    TiempoTranscurridoMantenimiento = 0;
                    TiempoMantenimiento = 0;
                    contadorPintados = 0;

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

                ONMaquinaEmpaquetadopictureBox.Visible = true;
                OFFMaquinaEmpaquetadopictureBox.Visible = false;

                TiempoEmpacando++;
                    
                if (TiempoEmpacando == (AuxEmpaque + 10))
                {
                    if (AuxEmpaque == 5)
                        AuxEmpaque = 0;

                    TiempoEmpacando = 0;

                    OFFMaquinaEmpaquetadopictureBox.Visible = true;
                    ONMaquinaEmpaquetadopictureBox.Visible = false;

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

        private void Marcos6PictureBox_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            timer1.Stop();

        }
    }
}
