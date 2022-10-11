using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_defence_MAIN
{
    public abstract class Objects_Game
    {

        #region variables

        protected PointF co_ords;
        // all game objects will have co ords and speeds
        protected float speeds;


        private Image sprites;
        protected List<Image> Frames_animation = new List<Image>();
        private float Speed_animation;                                      // all of these are lists of variables that will be use to handle scale factors and animations
        public float scale_factor;
        protected float Frameindex_current;

        #endregion


        #region GET/SET METHODS
        // these are the methods
        public float speed
        {
            get { return speeds; }
            set { speeds = value; }
        }

        public PointF Positionxy
        {
            get { return co_ords; }
            set { co_ords = value; }
        }

        public Image Sprite
        {
            get { return sprites; }
            set { sprites = value; }
        }

        //virtual is used when deriving methods into other classes
        public virtual RectangleF Hit_box
        {
            get
            {
                return new RectangleF(co_ords.X, co_ords.Y, sprites.Width * scale_factor, sprites.Height * scale_factor);
            }
        }

        #endregion

        //actual class
        public Objects_Game(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed)
        {
            // setting all of the parameters

            this.speeds = speed;
            this.co_ords = startposition;

            string[] image_files = image_file.Split(';');
            this.Frames_animation = new List<Image>();
            foreach (string path in image_files)
            {
                Frames_animation.Add(Image.FromFile(path));
            }
            this.sprites = this.Frames_animation[0];
            this.Speed_animation = animation_speed;
            this.scale_factor = scaleFactor;


        }



        public virtual void UpdateAnimation(float fps)
        {
            // this will work out the delta time and fps
            if (Speed_animation > 0)
            {
                float deltatime = 1 / fps;
                Frameindex_current += deltatime * Speed_animation;

                if (Frameindex_current >= Frames_animation.Count)
                {
                    Frameindex_current = 0;
                }

                Sprite = Frames_animation[(int)Frameindex_current];


            }


        }


        public virtual void Update(float deltatime)
        {
            // used to check for colisions
            Collision_Check();
        }




        public bool Colliding(Objects_Game other)
        {
            return Hit_box.IntersectsWith(other.Hit_box);
        }


        public void Collision_Check()
        {
            foreach (Objects_Game obj in GameScreen.Objects)
            {
                if (this.Colliding(obj))
                {
                    Collision_True(obj);
                }
            }
        }

        public virtual void Collision_True(Objects_Game other)
        {

        }



        public virtual void Draw(Graphics dc)
        {
            dc.DrawImage(sprites, co_ords.X, co_ords.Y, sprites.Width * scale_factor, sprites.Height * scale_factor);
        }
    }
}
