using System;
using Android.Support.V7.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Views;

namespace DroidKaigi2016Xamarin.Droid.Widgets
{
    public abstract class ArrayRecyclerAdapter<T> : RecyclerView.Adapter 
    {
        readonly Context context;
        readonly IList<T> list;
//        OnItemClickListener<T> onItemClickListener;
//        OnItemLongClickListener<T> onItemLongClickListener;

        public ArrayRecyclerAdapter(Context context) 
        {
            this.context = context;
            this.list = new List<T>();
        }

        public override int ItemCount
        {
            get { return list.Count; }
        }

        public void reset(ICollection<T> items) 
        {
            Clear();
            AddAll(items);
            NotifyDataSetChanged();
        }

        public T GetItem(int position) 
        {
            return list[position];
        }

        public void AddItem(T item) 
        {
            list.Add(item);
        }

        public void AddAll(ICollection<T> items) 
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
        }

//        public void AddAll(int position, Collection<T> items) 
//        {
//            list.addAll(position, items);
//        }
//
//        @UiThread
//        public void addAllWithNotification(Collection<T> items) {
//            int position = getItemCount();
//            addAll(items);
//            NotifyItemInserted(position);
//        }

        public void Clear() 
        {
            list.Clear();
        }

        public Context GetContext() 
        {
            return context;
        }

//        public void setOnItemClickListener(OnItemClickListener<T> listener) 
//        {
//            onItemClickListener = listener;
//            throw new NotImplementedException();
//        }
//
//        public void setOnItemLongClickListener(OnItemLongClickListener<T> listener) 
//        {
//            onItemLongClickListener = listener;
//            throw new NotImplementedException();
//        }
//
//        public void dispatchOnItemClick(View view, T item) 
//        {
//            assert onItemClickListener != null;
//            onItemClickListener.onItemClick(view, item);
//            throw new NotImplementedException();
//        }
//
//        public bool dispatchOnItemLongClick(View view, T item) 
//        {
//            assert onItemLongClickListener != null;
//            return onItemLongClickListener.onItemLongClick(view, item);
//            throw new NotImplementedException();
//        }
//
//        @Override
//        public Iterator<T> iterator() {
//            return list.iterator();
//        }
    }
}

