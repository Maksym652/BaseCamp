namespace Homework_02.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Repository for interraction with Point entities.
    /// </summary>
    public class PointRepository : IRepository<Point>
    {
        private static List<Point> points = new List<Point>();

        /// <summary>
        /// Creates new Point entity and adds it to the list.
        /// </summary>
        /// <param name="point">Point entity.</param>
        /// <returns>False if point id is less than 0 or if the list already contains point with the same Id, otherwise true.</returns>
        public bool Create(Point point)
        {
            if (point.Id < 0 || points.Find(p => p.Id == point.Id) != null)
            {
                return false;
            }

            points.Add(point);
            return true;
        }

        /// <summary>
        /// Removes point with specified id from the list.
        /// </summary>
        /// <param name="id">ID of the point to be deleted.</param>
        /// <returns>False if the list is not contain point with specified ID, otherwise true.</returns>
        public bool Delete(int id)
        {
            if (points.Find(p => p.Id == id) == null)
            {
                return false;
            }

            points.Remove(points.Find(p => p.Id == id));
            return true;
        }

        /// <summary>
        /// Returns the list of all points.
        /// </summary>
        /// <returns>Collection that contains all Point entities.</returns>
        public IEnumerable<Point> GetAll()
        {
            return points;
        }

        /// <summary>
        /// Returns point with specified id.
        /// </summary>
        /// <param name="id">ID of the point to be returned.</param>
        /// <returns>Point entity with specified ID.</returns>
        public Point GetById(int id)
        {
            return points.Find(p => p.Id == id);
        }

        /// <summary>
        /// Removes point that has the same ID as item, and adds item to the list.
        /// </summary>
        /// <param name="item">Item that replace point with same ID.</param>
        /// <returns>False if list not contains point with the same ID as item, otherwise true.</returns>
        public bool Update(Point item)
        {
            Point point = points.Find(p => p.Id == item.Id);
            if (point == null)
            {
                return false;
            }

            points.Remove(point);
            points.Add(item);
            return true;
        }
    }
}
